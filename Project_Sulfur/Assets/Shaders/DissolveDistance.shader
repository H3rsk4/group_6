Shader "Custom/DissolveBasedOnViewDistance" {
    Properties{
        _MainTex("Texture", 2D) = "white" {}
        _Center("Dissolve Center", Vector) = (0,0,0,0)
        _Interpolation("Dissolve Interpolation", Range(0,5)) = 0.8
        _DissTexture("Dissolve Texture", 2D) = "white" {}
    }

        SubShader{
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100

        Cull Off
        Lighting Off
        ZWrite Off

            CGPROGRAM

        //#pragma surface surf Standard vertex:vert
        #pragma surface surf Standard
        #pragma vertex vert

        //#pragma target 3.0

        struct Input {
            float2 uv_MainTex;
            float2 uv_DissTexture;
            float3 worldPos;
            float viewDist;
        };



        sampler2D _MainTex;
        sampler2D _DissTexture;
        half _Interpolation;
        float4 _Center;


        // Computes world space view direction
        // inline float3 WorldSpaceViewDir( in float4 v )
        // {
        //     return _WorldSpaceCameraPos.xyz - mul(_Object2World, v).xyz;
        // }


        void vert(inout appdata_full v,out Input o){
            UNITY_INITIALIZE_OUTPUT(Input,o);

         half3 viewDirW = WorldSpaceViewDir(v.vertex);
         o.viewDist = length(viewDirW);

        }

        void surf(Input IN, inout SurfaceOutputStandard o) {


            float l = length(_Center - IN.worldPos.xy);

            clip(saturate(IN.viewDist - l + (tex2D(_DissTexture, IN.uv_DissTexture) * _Interpolation * saturate(IN.viewDist))) - 0.5);

        o.Albedo = tex2D(_MainTex,IN.uv_MainTex);
        }
        ENDCG
        }
        Fallback "Diffuse"
}