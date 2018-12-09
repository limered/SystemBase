using StrongSystems.Audio.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using SystemBase;
using UniRx;
using UnityEngine;
using Object = UnityEngine.Object;

namespace StrongSystems.Audio
{
    [GameSystem]
    public class AudioSystem : GameSystem<SFXComponent, BackgroundMusicComponent>
    {
        private readonly BoolReactiveProperty _musicIsMuted = new BoolReactiveProperty(false);
        private readonly FloatReactiveProperty _musicVolume = new FloatReactiveProperty(1f);
        private readonly BoolReactiveProperty _sfxIsMuted = new BoolReactiveProperty(false);
        private readonly FloatReactiveProperty _sfxVolume = new FloatReactiveProperty(1f);

        public override void Init()
        {
            base.Init();

            MessageBroker.Default
                .Receive<AudioActSFXSetMute>()
                .Throttle(TimeSpan.FromMilliseconds(50))
                .Select(mute => mute.IsMuted)
                .Subscribe(b => _sfxIsMuted.Value = b);

            MessageBroker.Default
                .Receive<AudioActMusicSetMute>()
                .Throttle(TimeSpan.FromMilliseconds(50))
                .Select(mute => mute.IsMuted)
                .Subscribe(b => _musicIsMuted.Value = b);

            MessageBroker.Default
                .Receive<AudioActMusicSetVolume>()
                .Throttle(TimeSpan.FromMilliseconds(50))
                .Select(volume => volume.Volume)
                .Subscribe(newVolume => _musicVolume.Value = newVolume);

            MessageBroker.Default
                .Receive<AudioActSFXSetVolume>()
                .Throttle(TimeSpan.FromMilliseconds(50))
                .Select(volume => volume.Volume)
                .Subscribe(newVolume => _sfxVolume.Value = newVolume);
        }

        public override void Register(SFXComponent component)
        {
            MessageBroker.Default
                .Receive<AudioActSFXPlay>()
                .Where(_ => !_sfxIsMuted.Value)
                .DistinctUntilChanged(new SFXComparer())
                .Select(play => play.Name)
                .Subscribe(PlaySFX(component))
                .AddTo(component);
        }

        public override void Register(BackgroundMusicComponent component)
        {
            throw new System.NotImplementedException();
        }

        private static void RemoveSourceAfterStopped(AudioSource source)
        {
            Observable
                .Interval(TimeSpan.FromSeconds(1))
                .TakeWhile(_ => source.isPlaying)
                .Subscribe(_ => { }, () => Object.Destroy(source));
        }

        private Action<string> PlaySFX(SFXComponent component)
        {
            return name =>
            {
                var soundFile = component.Sounds.FirstOrDefault(file => file.Name == name);
                if (soundFile != null)
                {
                    var source = component.gameObject.AddComponent<AudioSource>();
                    source.pitch = 1 + (UnityEngine.Random.value - 0.5f) * 2f * component.MaxPitchChange;
                    source.PlayOneShot(soundFile.File, soundFile.Volume * _sfxVolume.Value);
                    RemoveSourceAfterStopped(source);
                }
                else
                {
                    Debug.Log("Can't find Sound with Name: " + name);
                }
            };
        }
    }

    public class SFXComparer : IEqualityComparer<AudioActSFXPlay>
    {
        public bool Equals(AudioActSFXPlay x, AudioActSFXPlay y)
        {
            return x != null &&
                   y != null &&
                   !string.IsNullOrEmpty(x.Tag) &&
                   !string.IsNullOrEmpty(y.Tag) &&
                   x.Tag.Equals(y.Tag);
        }

        public int GetHashCode(AudioActSFXPlay obj)
        {
            return obj.Tag.GetHashCode();
        }
    }
}