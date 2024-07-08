Shader "Custom/DisappearStandardShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _DisappearOffset ("Disappear Offset", Range(-2,2)) = -2
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows

        struct Input
        {
            float2 uv_MainTex;
            float3 worldPos;
        };

        sampler2D _MainTex;
        half _Glossiness;
        half _Metallic;
        float _DisappearOffset;
        fixed4 _Color;

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;

            // Metallic and smoothness
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;

            // Disappear effect
            clip(_DisappearOffset - IN.worldPos.y);
        }
        ENDCG
    }
    FallBack "Diffuse"
}
