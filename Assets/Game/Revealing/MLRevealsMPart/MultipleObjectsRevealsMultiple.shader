Shader "Reveal Object with Light"
{
	Properties
	{
		_Color("Color", Color) = (1, 1, 1, 1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_Glossiness("Smoothness", Range(0, 1)) = 0.5
		_Metallic("Metallic", Range(0, 1)) = 0.0
		_Alpha("Alpha", Range(0, 1)) = 0.0

		_EmissionColor("Emission Color1", Color) = (1, 1, 1, 1)
		_LightPosition("Light Position", Vector) = (1, 1, 1, 1)
		_LightDirection("Light Direction", Vector) = (1, 1, 1, 1)
		_LightAngle("Light Angle", Float) = 1
		_LightRange("Light Range", Float) = 1
		_StrengthDistance("Strength Distance", Float) = 1
		_StrengthScalar("Strength Scalar", Float) = 10

		_EmissionColor2("Emission Color2", Color) = (1, 1, 1, 1)
		_LightPosition2("Light Position2", Vector) = (1, 1, 1, 1)
		_LightDirection2("Light Direction2", Vector) = (1, 1, 1, 1)
		_LightAngle2("Light Angle2", Float) = 1
		_LightRange2("Light Range2", Float) = 1
		_StrengthDistance2("Strength Distance2", Float) = 1
		_StrengthScalar2("Strength Scalar2", Float) = 10

		_EmissionColor3("Emission Color3", Color) = (1, 1, 1, 1)
		_LightPosition3("Light Position3", Vector) = (1, 1, 1, 1)
		_LightDirection3("Light Direction3", Vector) = (1, 1, 1, 1)
		_LightAngle3("Light Angle3", Float) = 1
		_LightRange3("Light Range3", Float) = 1
		_StrengthDistance3("Strength Distance3", Float) = 1
		_StrengthScalar3("Strength Scalar3", Float) = 10
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
			fixed4 _EmissionColor;
			float4 _LightPosition;
			float4 _LightDirection;
			float  _LightAngle;
			float _LightRange;
			float _StrengthDistance;
			float  _StrengthScalar;

			fixed4 _EmissionColor2;
			float4 _LightPosition2;
			float4 _LightDirection2;
			float  _LightAngle2;
			float _StrengthDistance2;
			float  _StrengthScalar2;
			
			fixed4 _EmissionColor3;
			float4 _LightPosition3;
			float4 _LightDirection3;
			float  _LightAngle3;
			float _StrengthDistance3;
			float  _StrengthScalar3;


			void SurfaceReveal(Input input, inout SurfaceOutputStandard R)
			{
				float3 Dir = normalize(_LightPosition - input.worldPos);
				float  Scale = dot(Dir, _LightDirection);
				float  Strength = clamp(Scale - cos(_LightAngle * (UNITY_PI / 360.0)), 0, 1);
				_StrengthDistance = clamp(_LightRange / distance(_LightPosition, input.worldPos), 0, 1);
				Strength = clamp(Strength * _StrengthScalar * _StrengthDistance, 0, 1);
				
				float3 Dir2 = normalize(_LightPosition2 - input.worldPos);
				float  Scale2 = dot(Dir2, _LightDirection2);
				float  Strength2 = clamp(Scale2 - cos(_LightAngle2 * (UNITY_PI / 360.0)), 0, 1);
				_StrengthDistance2 = clamp(_LightRange / distance(_LightPosition2, input.worldPos), 0, 1);
				Strength2 = clamp(Strength2 * _StrengthScalar2 * _StrengthDistance2, 0, 1);
				
				float3 Dir3 = normalize(_LightPosition3 - input.worldPos);
				float  Scale3 = dot(Dir3, _LightDirection3);
				float  Strength3 = clamp(Scale3 - cos(_LightAngle3 * (UNITY_PI / 360.0)), 0, 1);
				_StrengthDistance3 = clamp(_LightRange / distance(_LightPosition3, input.worldPos), 0, 1);
				Strength3 = clamp(Strength3 * _StrengthScalar3 * _StrengthDistance3, 0, 1);
				
				fixed4 RC = tex2D(_MainTex, input.UVMainTex) * _Color;
				R.Albedo = RC.rgb;
				R.Metallic = _Metallic;
				R.Smoothness = _Glossiness;
				R.Alpha = clamp((Strength +  Strength2 + Strength3), 0, 1) * RC.a;
				R.Emission = ((Strength * (_EmissionColor * 10)) + (Strength2 * (_EmissionColor2 * 10)) + (Strength3 * (_EmissionColor3 * 10))) / 3;

			}
		ENDCG
		}
	FallBack "Diffuse"
}