Shader "Custom/DropShadow_DissolveDistance"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
		_ShadowColor ("Shadow", Color) = (0,0,0,1)
		_ShadowOffset ("ShadowOffset", Vector) = (0,-0.1,0,0)
		_Rotation("Rotation", Float) = 0.0
		_Scale("Scale", Float) = 0.0
		_Amplitude("Amplitude", Float) = 0.0
        _DissolveTexture("Dissolve texture", 2D) = "white" {}
        _Radius("Distance", Float) = 1
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

		// draw shadow
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
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				float2 texcoord  : TEXCOORD0;
			};
			
			fixed4 _Color;
			fixed4 _ShadowColor;
			float4 _ShadowOffset;
			
			
			float4 textureOffset;

			sampler2D _MainTex;
			float4 _MainTex_TexelSize;
			

			float _Rotation;
			float _Scale;
			float _Amplitude;

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				//textureOffset = float4(0,(-_MainTex_TexelSize.y),0,0);
				//IN.vertex.xy -= .5;
				const float Deg2Rad = (UNITY_PI * 2.0) / 360.0;
				float rotationRadians = -_Rotation * Deg2Rad;

				float sinX = sin (rotationRadians);
				float cosX = cos (rotationRadians);
				float sinY = sin (rotationRadians);

				float2x2 rotationMatrix = float2x2(cosX, -sinX, sinX, cosX);
				//float2x2 rotationMatrix = float2x2(cosX, 0, sinX, cosX);

				rotationMatrix *= 0.5;
                rotationMatrix += 0.5;
				rotationMatrix = rotationMatrix * 2-1;
				_ShadowOffset = float4(_ShadowOffset.x, _ShadowOffset.y + ((-_MainTex_TexelSize.w + (_MainTex_TexelSize.w % 1)) / 32) - (_MainTex_TexelSize.w % 1), 0, 0);
				
				//IN.vertex.xy += .5;
				//IN.vertex.x += sin(-IN.vertex.y * (32 / (_MainTex_TexelSize.w)));
				//IN.vertex.x += sin(-IN.vertex.y * _Scale) * _Amplitude;
				OUT.vertex = UnityObjectToClipPos(IN.vertex + _ShadowOffset);
				//OUT.vertex = OUT.vertex * _Scale;
				

				OUT.texcoord = IN.texcoord;
				OUT.texcoord.xy -= .5;
				OUT.texcoord.xy = mul (OUT.texcoord.xy, rotationMatrix);
				OUT.texcoord.xy += .5;
				//OUT.vertex.xy = OUT.vertex.xy * _Scale;


				OUT.color = IN.color *_ShadowColor;
				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap (OUT.vertex);
				#endif
				
				/*
				OUT.texcoord.xy -= .5;
				const float Deg2Rad = (UNITY_PI * 2.0) / 360.0;
				float rotationRadians = -_Rotation * Deg2Rad;

				float sinX = sin (rotationRadians);
				float cosX = cos (rotationRadians);
				float sinY = sin (rotationRadians);
				

				float2x2 rotationMatrix = float2x2(cosX, -sinX, sinX, cosX);
				rotationMatrix *=0.5;
                rotationMatrix +=0.5;
                rotationMatrix = rotationMatrix * 2-1;
				OUT.texcoord.xy = mul ( OUT.texcoord.xy, rotationMatrix);
				OUT.texcoord.xy += .5;
				//OUT.texcoord.xy = OUT.texcoord.xy * _Scale;
				*/

				


				return OUT;
			}

			//sampler2D _MainTex;
			//float4 _MainTex_TexelSize;
			sampler2D _AlphaTex;
			
			float _AlphaSplitEnabled;

			fixed4 SampleSpriteTexture (float2 uv)
			{
				uv.x = 1.0 - uv.x;
				//uv = (uv - 0.5) / _Scale + 0.5;
				fixed4 color = tex2D (_MainTex, uv);
				color.rgb = _ShadowColor.rgb;

				

				#if UNITY_TEXTURE_ALPHASPLIT_ALLOWED
				if (_AlphaSplitEnabled)
					color.a = tex2D (_AlphaTex, uv).r;
				#endif //UNITY_TEXTURE_ALPHASPLIT_ALLOWED

				return color;
			}

			fixed4 frag(v2f IN) : SV_Target
			{
				fixed4 c = SampleSpriteTexture (IN.texcoord) * IN.color;
				c.rgb *= c.a;
				return c;
			}
		ENDCG
		}

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
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
                float3 worldPos : TEXCOORD1;
			};
			
			fixed4 _Color;

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color * _Color;
				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap (OUT.vertex);
				#endif

                OUT.worldPos = mul(unity_ObjectToWorld, IN.vertex);

				return OUT;
			}

			sampler2D _MainTex;
			sampler2D _AlphaTex;
			float _AlphaSplitEnabled;
            sampler2D _DissolveTexture;
            float _Radius;
            float3 _PlayerPos;
            //float3 worldPos;
            
			

			fixed4 SampleSpriteTexture (float2 uv)
			{
				fixed4 color = tex2D (_MainTex, uv);

				#if UNITY_TEXTURE_ALPHASPLIT_ALLOWED
				if (_AlphaSplitEnabled)
					color.a = tex2D (_AlphaTex, uv).r;
				#endif //UNITY_TEXTURE_ALPHASPLIT_ALLOWED

				return color;
			}

			fixed4 frag(v2f IN) : SV_Target
			{
                half dissolve_value = tex2D(_DissolveTexture, IN.texcoord).x;
                float dist = _Radius - distance(_PlayerPos, IN.worldPos);
                //float dist = distance(_PlayerPos, IN.worldPos); //non-inverted
                //clip(dissolve_value - dist / _Radius);

                if((dissolve_value - dist / _Radius) < .5f){
                    IN.color.a = .5f;
                    //IN.color.r = 1;
                    //IN.color.g = 0;
                    //IN.color.b = 0;
                }
				fixed4 c = SampleSpriteTexture (IN.texcoord) * IN.color;
				c.rgb *= c.a;
				return c;
			}
		ENDCG
		}
	}
}