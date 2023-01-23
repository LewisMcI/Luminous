Shader "Reveal Object with Light"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
		_Alpha("Alpha", Range(0,1)) = 0.0
		_LightDirection("Light Direction", Vector) = (0,0,1,0)
		_LightPosition("Light Position", Vector) = (0,0,0,0)
		_LightAngle("Light Angle", Range(0,180)) = 45
		_StrengthScalar("Strength", Float) = 50
		_StrengthDistance("StrengthDist", Float) = 0
		_RequirementsMet("Visible", Float) = 0
	}
		SubShader
		{
			Tags{ "RenderType" = "Transparent" "Queue" = "Transparent" }
			Blend SrcAlpha OneMinusSrcAlpha
			LOD 200
			CGPROGRAM
			#pragma surface SurfaceReveal Standard fullforwardshadows alpha:fade
			#pragma target 3.0

			sampler2D _MainTex;
			struct Input
			{
				float2 UVMainTex;
				float3 worldPos;
			};//Struct end

			half   _Glossiness;
			half   _Metallic;
			fixed4 _Color;
			half Alpha;
			float4 _LightPosition;
			float4 _LightDirection;
			float  _LightAngle;
			float  _StrengthScalar;
			float  _StrengthDistance;

			void SurfaceReveal(Input input, inout SurfaceOutputStandard R)
			{
				float3 Dir = normalize(_LightPosition - input.worldPos);
				float  Scale = dot(Dir, _LightDirection);
				float  Strength = clamp(Scale - cos(_LightAngle * (UNITY_PI / 360.0)), 0, 1);
				Strength = clamp(Strength * _StrengthScalar * _StrengthDistance, 0, 1);
				fixed4 RC = tex2D(_MainTex, input.UVMainTex) * _Color;
				R.Albedo = RC.rgb;
				R.Metallic = _Metallic;
				R.Smoothness = _Glossiness;
				R.Alpha = Strength * RC.a;

			}
		ENDCG
		}
	FallBack "Diffuse"
}