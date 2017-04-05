// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

#warning Upgrade NOTE: unity_Scale shader variable was removed; replaced 'unity_Scale.w' with '1.0'

Shader "Q5/Proj/effect/Sea" { 
Properties {
	//_BaseColor ("Base color", COLOR)  = ( .54, .95, .99, 0.5)	
	_ColorProgress("Color progress", Range(1.0, 10.0)) = 2.0
	_CenterColor1 ("Center color1", COLOR)  = ( 1.0, 1.0, 1.0, .5)	
	_EdgeColor1 ("Edge color1", COLOR)  = ( .5, .5, .5, 0.5)	
	_CenterColor2 ("Center color2", COLOR)  = ( .5, .5, .5, 0.5)	
	_EdgeColor2 ("Edge color2", COLOR)  = ( .5, .5, .5, 0.5)	

	_TexSpeed("UV Speed-------------", Vector) = (0.0, 0.0, 0.0, 0.0)

	_NormalTex ("Normal texture----------", 2D) = "white" {}	
	_NormalMovement ("Normal wave period", Float) = 5.5 

	_MainTexEnable ("enable main texture-------------", int) = 0
	_MainTex ("Main texture", 2D) = "white" {}	
	//_LightMapTex ("LightMap texture", 2D) = "white" {}	
	//_BackgroundTex ("Background texture", 2D) = "black" {}	
	//_BackgroundAlphaOffset ("Background alpha offset", Float) = 0

	_SpecularEnable("enable specular---------------", int) = 0
	_SpecularColor ("Specular color", COLOR)  = ( 1.0, 1.0, 1.0, 1.0)	
	_Shiness ("Shiness:minAngle:maxAngle,Power:offPower", Vector)  = (0, 15, 10, 5)
	_ShinessIntensity ("Gloss intensity", Float) = 1.0
	
	_VertexAniEnable ("enable vertex animation-------------", int) = 0 
	//_GerstnerIntensity ("Per vertex displacement", Float) = 1.0
	/*
	_GAmplitude ("Wave Amplitude", Vector) = (0.3 ,0.35, 0.25, 0.25)
	_GFrequency ("Wave Frequency", Vector) = (1.3, 1.35, 1.25, 1.25)
	_GSteepness ("Wave Steepness", Vector) = (1.0, 1.0, 1.0, 1.0)
	_GSpeed ("Wave Speed", Vector) = (1.2, 1.375, 1.1, 1.5)
	_GDirectionAB ("Wave Direction A B", Vector) = (0.3 ,0.85, 0.85, 0.25)
	_GDirectionCD ("Wave Direction C D", Vector) = (0.1 ,0.9, 0.5, 0.5)	
	*/
	_VertexWaveArg ("Wave Amplitude:Length:Speed:Steepnes(0-1)", Vector) = (4 ,120, 3, 1)
	_VertexWaveDir ("Wave Direction X:Y:0:0", Vector) = (1 ,1, 0.0, 0.0)
	
	_1_width("sea _1_width", Float) = 0.016
	_1_height("sea _1_height", Float) = 0.016
	_offsetX("sea offsetX", Float) = 30
	_offsetZ("sea offsetZ", Float) = 30
}


Subshader 
{ 	
	//Tags {"RenderType"="Opaque" "Queue"="Background"}
	Tags {"RenderType"="Transparent" "Queue"="Transparent"}
	
	Lod 200
	//ColorMask RGB

	Pass {
		Tags { "LightMode" = "ForwardBase" }

			Blend SrcAlpha OneMinusSrcAlpha
			ZTest LEqual
			ZWrite Off
			Cull Off
			
			CGPROGRAM
			
			#pragma target 2.0 
			#pragma vertex vert200
			#pragma fragment frag200
			//#pragma only_renderers gles  

	#include "UnityCG.cginc"

	//uniform float4 _BaseColor;
	uniform float _ColorProgress;
	uniform float4 _CenterColor1;
	uniform float4 _EdgeColor1;
	uniform float4 _CenterColor2;
	uniform float4 _EdgeColor2;
	uniform float4 _TexSpeed;
	uniform float _ShinessIntensity;
	uniform float _NormalMovement;
	uniform float _BackgroundAlphaOffset;

	int _MainTexEnable;
	uniform sampler2D _MainTex;
	uniform float4 _MainTex_ST;

	int _SpecularEnable;
	uniform float4 _SpecularColor;
	uniform float4 _Shiness;

	uniform sampler2D _NormalTex;
	uniform float4 _NormalTex_ST;
	uniform sampler2D _LightMapTex;
	uniform float4 _LightMapTex_ST;
	uniform sampler2D _BackgroundTex;
	uniform float4 _BackgroundTex_ST;

	int _VertexAniEnable;
	//uniform half _GerstnerIntensity;
	uniform float4 _GAmplitude;
	uniform float4 _GFrequency;
	uniform float4 _GSteepness; 									
	uniform float4 _GSpeed;					
	uniform float4 _GDirectionAB;		
	uniform float4 _GDirectionCD;

	uniform float4 _VertexWaveArg;
	uniform float4 _VertexWaveDir;
	

	//uniform fixed4 _LightColor0;
	uniform float4 _dirLightColor;
	uniform float4 _dirLightWorldForward;
	uniform float _dirLightIntensity;


	uniform float _1_width;
	uniform float _1_height;
	uniform float _offsetX;
	uniform float _offsetZ;

	struct appdata_t {
		float4 vertex : POSITION;
		fixed4 color : COLOR;
	};

	struct v2f
	{
		float4 pos : SV_POSITION;
		fixed4 color1 : COLOR;
		float4 texcoord : TEXCOORD0;
		float4 texcoord1 : TEXCOORD1;
		float4 normal : TEXCOORD2;
		float4 color2 : TEXCOORD3;
	};
		
	

	half3 GerstnerOffset4(half2 xzVtx, half4 steepness, half4 amp, half4 freq, half4 speed, half4 dirAB, half4 dirCD) 
	{
		half3 offsets;
		
		half4 AB = steepness.xxyy * amp.xxyy * dirAB.xyzw;
		half4 CD = steepness.zzww * amp.zzww * dirCD.xyzw;
		
		half4 dotABCD = freq.xyzw * half4(dot(dirAB.xy, xzVtx), dot(dirAB.zw, xzVtx), dot(dirCD.xy, xzVtx), dot(dirCD.zw, xzVtx));
		half4 TIME = _Time.yyyy * speed;
		
		half4 COS = cos (dotABCD + TIME);
		half4 SIN = sin (dotABCD + TIME);
		
		offsets.x = dot(COS, half4(AB.xz, CD.xz));
		offsets.z = dot(COS, half4(AB.yw, CD.yw));
		offsets.y = dot(SIN, amp);

		return offsets;			
	}
	

	inline half3 Gerstner (half3 vtx, half3 tileableVtx, 
					 half4 amplitude, half4 frequency, half4 steepness, 
					 half4 speed, half4 directionAB, half4 directionCD ) 
	{
		return GerstnerOffset4(tileableVtx.xz, steepness, amplitude, frequency, speed, directionAB, directionCD);

	}

	inline half3 Gerstner1 (half3 worldvtx, half4 color)
	{
		half3 offsets;
		half3 wavedir = half3(_VertexWaveDir.x, 0, _VertexWaveDir.y);
		half2 dir = normalize(wavedir).xz;

		half amplitude = _VertexWaveArg.x;
		half length = _VertexWaveArg.y;
		half speed = _VertexWaveArg.z;
		half steepness = _VertexWaveArg.w;
		half pi = 3.14;

		float angle = (dot(dir, worldvtx.xz) + _Time.y * speed) * 2 * pi / length;
		offsets.y = amplitude * sin(angle);
		offsets.xz = dir * steepness * amplitude * cos(angle); 

		return offsets;			
	}
	

	v2f vert200(appdata_t v)
	{
		v2f o;
		
		half3 worldSpaceVertex = mul(unity_ObjectToWorld,(v.vertex)).xyz;

		if (_VertexAniEnable != 0) {
			half3 vtxForAni = (worldSpaceVertex).xzz * 1.0; 			
			/*
			half3 offsets = Gerstner (v.vertex.xyz, vtxForAni, 					// offsets, nrml will be written
				_GAmplitude,					 							// amplitude
				_GFrequency,				 								// frequency
				_GSteepness, 												// steepness
				_GSpeed,													// speed
				_GDirectionAB,												// direction # 1, 2
				_GDirectionCD												// direction # 3, 4
			);
			*/
			half3 offsets = Gerstner1(vtxForAni, v.color);
			v.vertex.xyz += offsets;		
		}

		o.pos = mul(UNITY_MATRIX_MVP, v.vertex);

		float2 xz = v.vertex.xz + float2(_offsetX, _offsetZ);
		float2 uv = xz * float2(_1_width, _1_height);
		float4 uvOffset = _Time.yyyy * _TexSpeed; 

		float2 mainTexUV = uv + uvOffset.xy;
		float2 normalTexUV = uv + uvOffset.zw;
		float2 lightmapTexUV = uv;
		float2 backgroundTexUV = uv;

		o.texcoord.xy = mainTexUV.xy * _MainTex_ST.xy + _MainTex_ST.zw;
		o.texcoord.zw = normalTexUV.xy * _NormalTex_ST.xy + _NormalTex_ST.zw;
		o.texcoord1.xy = lightmapTexUV.xy * _LightMapTex_ST.xy + _LightMapTex_ST.zw;
		o.texcoord1.zw = backgroundTexUV.xy * _BackgroundTex_ST.xy + _BackgroundTex_ST.zw;


		float blender = cos(v.vertex.x + v.vertex.z + v.vertex.y + _Time.y * 6.28 /_NormalMovement);
		o.normal.x = (blender + 1.0) * 0.5;
		
		float dxy2 = dot(o.pos.xy, o.pos.xy);
		float m =  dxy2/(2 * _ColorProgress);
		float dm = 1 - m;

		//o.color1 = _CenterColor1 * dm + _EdgeColor1 * m ;
		//o.color2 = _CenterColor2 * dm + _EdgeColor2 * m;
		o.color1 = _CenterColor1;
		o.color2 = _CenterColor2;
		if (_SpecularEnable != 0) {
			float minangle = _Shiness.x * 3.14/180;
			float maxangle = _Shiness.y * 3.14/180;
			float span = maxangle - minangle;
			float angle = minangle + span * dxy2/2.0;
			o.normal.y = cos(angle);

			o.normal.z = _Shiness.z + o.normal.y * _Shiness.w;
		}

		return o;
	}

	float4 frag200(v2f i) : COLOR
	{		

		float4 normal = tex2D(_NormalTex, i.texcoord.zw);
		float2 n = float2(i.normal.x, 1.0 - i.normal.x);
		float diff = dot(normal.xy, n);
		//c.rgb *= diff;

		float4 c = i.color1 * (1.0 - diff) + i.color2 * diff;
		
		if (_MainTexEnable != 0) {
			float4 main = tex2D(_MainTex, i.texcoord.xy);
			//c *= main;
			
			//c.rgb = c.rgb * (1 - main.a) + main.rgb * main.a;
			//c.rgb = c.rgb + main.rgb;
			c.rgb += main.rgb * main.a;

		}
			
		if (_SpecularEnable != 0) {
			float nh = clamp(i.normal.y * dot(normal.zw, n), 0.0, 1.0);
			float spec = max(0.0, pow(nh, i.normal.z));
			c.rgb += _SpecularColor.rgb * spec *  _ShinessIntensity;
		}

		return c;
	}
						  			
			ENDCG
	}	
}

//Fallback "Transparent/Diffuse"
}
