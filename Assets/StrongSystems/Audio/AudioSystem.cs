using StrongSystems.Audio.Actions;
using System;
using System.Linq;
using SystemBase;
using StrongSystems.Audio.Helper;
using UniRx;
using UnityEngine;
using Utils;
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

        private BackgroundMusicComponent _currentMusic;
        private BackgroundMusicComponent _lastMusic;

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
                .DistinctUntilChanged(IoC.Resolve<ISFXComparer>())
                .Select(play => play.Name)
                .Subscribe(PlaySFX(component))
                .AddTo(component);
        }

        public override void Register(BackgroundMusicComponent component)
        {
            MessageBroker.Default
                .Receive<AudioActMusicStart>()
                .Subscribe(start =>
                {
                })
                .AddTo(component);

            MessageBroker.Default
                .Receive<AudioActMusicStop>()
                .Subscribe(stop => { })
                .AddTo(component);

            _musicIsMuted
                .Subscribe(b => component.Source.mute = b)
                .AddTo(component);

            _musicVolume
                .Subscribe(f => component.Source.volume = f)
                .AddTo(component);
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
}