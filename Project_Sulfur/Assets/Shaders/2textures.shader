Shader "UnityCommunity/Sprites/2Textures"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _ShadowTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
		_ShadowColor ("Shadow", Color) = (0,0,0,1)
		_ShadowOffset ("ShadowOffset", Vector) = (0,-0.1,0,0)
		_Rotation("Rotation", Float) = 0.0
		_Scale("Scale", Float) = 0.0
	}

	SubShader
	{
		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha


		// draw real sprite
		Pass
		{
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile _ PIXELSNAP_ON
			#include "UnityCG.cginc"
			
			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
                float2 texcoord2 : TEXCOORD1;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				float2 texcoord  : TEXCOORD0;
                float2 texcoord2 : TEXCOORD1;
			};
			
			fixed4 _Color;
            float _Scale;

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord * _Scale;
                OUT.texcoord2 = IN.texcoord2;
				OUT.color = IN.color * _Color;
				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap (OUT.vertex);
				#endif

				return OUT;
			}

			sampler2D _MainTex;
            sampler2D _ShadowTex;
			sampler2D _AlphaTex;
			float _AlphaSplitEnabled;
			

			fixed4 SampleSpriteTexture (float2 uv, sampler2D _Texture)
			{
				fixed4 color = tex2D (_Texture, uv);

				#if UNITY_TEXTURE_ALPHASPLIT_ALLOWED
				if (_AlphaSplitEnabled)
					color.a = tex2D (_AlphaTex, uv).r;
				#endif //UNITY_TEXTURE_ALPHASPLIT_ALLOWED

				return color;
			}

			fixed4 frag(v2f IN) : SV_Target
			{
				fixed4 c = SampleSpriteTexture (IN.texcoord, _ShadowTex) * IN.color;
                fixed4 c2 = SampleSpriteTexture (IN.texcoord2, _MainTex) * IN.color;
                fixed4 returnTexture = c;
                returnTexture.rgba = lerp(c,c2,c2.a).rgba;
				return returnTexture;
			}
		ENDCG
		}
	}
}