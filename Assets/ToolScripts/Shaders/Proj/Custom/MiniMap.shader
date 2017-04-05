Shader "Q5/Proj/Custom/MiniMap" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_FogTex0 ("Fog 0", 2D) = "white" {}
		_FogTex1 ("Fog 1", 2D) = "white" {}
		_Unexplored ("Unexplored Color", Color) = (0.05, 0.05, 0.05, 1.0)
		_Explored ("Explored Color", Color) = (0.35, 0.35, 0.35, 1.0)
	}
	
	SubShader {	
		Pass
		{
			ZTest Always
			Cull Off
			ZWrite Off
			Fog { Mode off }
			Blend SrcAlpha OneMinusSrcAlpha
			
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag vertex:vert
			#pragma fragmentoption ARB_precision_hint_fastest
			#include "UnityCG.cginc"
			
			sampler2D _MainTex;
			sampler2D _FogTex0;
			sampler2D _FogTex1;
			
			uniform float _blendFactor;
			uniform half4 _Unexplored;
			uniform half4 _Explored;
			
			struct Input
			{
				float4 position : POSITION;
				float2 uv : TEXCOORD0;
			};
			
			void vert (inout appdata_full v, out Input o)
			{
				o.position = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.texcoord.xy;	
			}
			
			fixed4 frag (Input i) : COLOR
			{
				half4 original = tex2D(_MainTex, i.uv);
				half4 fog = lerp(tex2D(_FogTex0, i.uv), tex2D(_FogTex1, i.uv), _blendFactor);
				return lerp(lerp(original * _Unexplored, original * _Explored, fog.g), original, fog.r);
			}
			ENDCG
		}
	}
	
	FallBack off
}
