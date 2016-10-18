// author: lxt  created: 2015/8/10
// descrip: 用于大块地表纹理的混合显示，提高纹理的显示精度
Shader "Q5/Proj/Custom/TerrainBlend" {
	Properties {
		_Control ("Control(RGB)", 2D) = "red"{}//控制贴图
		//_Splat3 ("Layer3(A)",2D) = "white" {} //alpha通道控制的贴图
		_Splat2 ("Layer2(B)",2D) = "white" {} //B通道控制的贴图
		_Splat1 ("Layer1(G)",2D) = "white" {} //G通道控制的贴图
		_Splat0 ("Layer0(R)",2D) = "white" {} //R通道控制的贴图
	}
	SubShader {
		Tags {
			"SplatCount" = "3"
			"Queue" = "Geometry-100"
		 	"RenderType"="Opaque" 
		}
		//LOD 200
		CGPROGRAM
		//#pragma target 4.0
		#pragma surface surf Lambert noforwardadd
		
		sampler2D _Control;
		sampler2D _Splat0,_Splat1,_Splat2;//, _Splat3;

		struct Input {
			float2 uv_Control;
			float2 uv_Splat0;
			float2 uv_Splat1;
			float2 uv_Splat2;
			//float2 uv_Splat3;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			fixed4 splat_control = tex2D (_Control, IN.uv_Control);
			fixed3 col;
			col = splat_control.r * tex2D(_Splat0, IN.uv_Splat0).rgb;
			col += splat_control.g * tex2D(_Splat1, IN.uv_Splat1).rgb;
			col += splat_control.b * tex2D(_Splat2, IN.uv_Splat2).rgb;
			//fixed alpha = 1 - splat_control.r - splat_control.g - splat_control.b;
			//col += alpha * tex2D(_Splat3, IN.uv_Splat3).rgb;
			o.Albedo = col;
			o.Alpha = 1.0;
		}
		ENDCG
	} 
	FallBack "Q5/Unity/Mobile/Diffuse"
}
