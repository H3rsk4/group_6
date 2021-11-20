// https://sudonull.com/post/9435-Shaders-of-dissolution-and-exploration-of-the-world

Shader "Custom/GlobalDissolveSprites"
{
 Properties
 {
 [PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
 _Color("Tint", Color) = (1,1,1,1)
 [MaterialToggle] PixelSnap("Pixel snap", Float) = 0
 [HideInInspector] _RendererColor("RendererColor", Color) = (1,1,1,1)
 [HideInInspector] _Flip("Flip", Vector) = (1,1,1,1)
 [PerRendererData] _AlphaTex("External Alpha", 2D) = "white" {}
 [PerRendererData] _EnableExternalAlpha("Enable External Alpha", Float) = 0
 _DissolveTexture("Dissolve texture", 2D) = "white" {}
 _Radius("Distance", Float) = 1 //distance where we startto reveal the objects
 }

 
 SubShader
 {
 Tags
 {
 "Queue" = "Transparent"
 "IgnoreProjector" = "True"
 "RenderType" = "Transparent"
 "PreviewType" = "Plane"
 "CanUseSpriteAtlas" = "True"
 }
 Cull Off
 Lighting Off
 ZWrite Off
 Blend One OneMinusSrcAlpha

 CGPROGRAM
 #pragma surface surf Lambert vertex:vert nofog nolightmap nodynlightmap keepalpha noinstancing
 #pragma multi_compile _ PIXELSNAP_ON
 #pragma multi_compile _ ETC1_EXTERNAL_ALPHA
 #include "UnitySprites.cginc"

 struct Input
 {
 float2 uv_MainTex;
 fixed4 color;
 float3 worldPos; //Built-in world position
 };
 sampler2D _DissolveTexture; //texture where we get the dissolve value
 float3 _PlayerPos; //"Global Shader Variable", contains the Player Position
 float _Radius;
 void vert(inout appdata_full v, out Input o)
 {
 v.vertex = UnityFlipSprite(v.vertex, _Flip);
 #if defined(PIXELSNAP_ON)
 v.vertex = UnityPixelSnap(v.vertex);
 #endif
 UNITY_INITIALIZE_OUTPUT(Input, o);
 o.color = v.color * _Color * _RendererColor;
 }
 void surf(Input IN, inout SurfaceOutput o)
 {
 half dissolve_value = tex2D(_DissolveTexture, IN.uv_MainTex).x;
 float dist = _Radius - distance(_PlayerPos, IN.worldPos);
 //float dist = distance(_PlayerPos, IN.worldPos); //non-inverted
 //clip(dissolve_value - dist / _Radius);

 if((dissolve_value - dist / _Radius) < .5f){
    IN.color.a = .5f;
    //IN.color.r = 1;
    //IN.color.g = 0;
    //IN.color.b = 0;
 }


 fixed4 c = SampleSpriteTexture(IN.uv_MainTex) * IN.color;
 o.Albedo = c.rgb * c.a;
 o.Alpha = c.a;
 }
 ENDCG
 }
 Fallback "Transparent/VertexLit"
}