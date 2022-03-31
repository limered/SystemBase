// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/YAFG_low_poly" {
    Properties{
        _LightDirection("LightDirection", Vector) = (.0, .0, .0, .0)
        _LightColor("LightColor", Color) = (.0, 0.5, .0, 1.0)
     }
 
     SubShader {
         Tags { "RenderType"="Opaque" }
         LOD 200
 
         Pass{
             CGPROGRAM
 
             #pragma target 3.0
             #pragma vertex vert
             #pragma fragment frag
 
             #include "UnityCG.cginc"
 
             struct v2f 
             {
                 float4 pos  : POSITION;
                 float2 uv   : TEXCOORD0;
                 float3 wpos : TEXCOORD1;
                 float3 vpos : TEXCOORD2;
             };
             
             v2f vert( appdata_img v )
             {
                 v2f o;
                 o.pos = UnityObjectToClipPos(v.vertex);
                 o.wpos = mul (unity_ObjectToWorld, v.vertex).xyz;
                 o.vpos = v.vertex.xyz;
                 o.uv =  v.texcoord.xy;
                 return o;
             }

             uniform float3 _LightDirection;
             uniform fixed4 _LightColor;
         
             fixed4 frag(v2f i) : SV_Target {
                 
                 float3 normal = normalize(cross(ddy(i.wpos), ddx(i.wpos)));
                 float4 color = dot(normal, _LightDirection);
                 
                 return color * _LightColor;
             }
 
             ENDCG
         }
     }
    FallBack "Diffuse"
}