Shader "Skybox/Gradient Skybox Shader"
{
	Properties
	{
		_Color1("Top Color", Color) = (1, 1, 1, 0)
		_Color2("Bottom Color", Color) = (1, 1, 1, 0)
		_UpVector("Up Vector", Vector) = (0, 1, 0, 0)

	}

		CGINCLUDE

#include "UnityCG.cginc"

		struct appdata
	{
		float4 position : POSITION;
		float3 texcoord : TEXCOORD0;
	};

	struct v2f
	{
		float4 position : SV_POSITION;
		float3 texcoord : TEXCOORD0;
	};

	static const half4 _Up = (0, 1, 0, 0);
	fixed4 _Color1;
	fixed4 _Color2;
	half4 _UpVector;

	v2f vert(appdata v)
	{
		v2f o;
		o.position = UnityObjectToClipPos(v.position);
		o.texcoord = v.texcoord;
		return o;
	}

	fixed4 frag(v2f i) : COLOR
	{
		half d = dot(normalize(i.texcoord), _UpVector) * 0.5f + 0.5f;
		return lerp(_Color2, _Color1, d);
	}

		ENDCG

		SubShader
	{
		Tags{ "RenderType" = "Background" "Queue" = "Background" }

			Pass
		{
			ZWrite Off
			Cull Off
			Fog { Mode Off }
			CGPROGRAM
			#pragma fragmentoption ARB_precision_hint_fastest
			#pragma vertex vert
			#pragma fragment frag
			ENDCG
		}
	}

}
