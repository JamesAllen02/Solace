// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "James/speedLines"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		[HDR]_Colour("Colour", Color) = (1,1,1,1)
		_SpeedLinesTiling("Speed Lines Tiling", Float) = 200
		_SpeedLinesRadialScale("Speed Lines Radial Scale", Range( 0 , 10)) = 0.1
		_SpeedLinesPower("Speed Lines Power", Float) = 1
		_SpeedLinesRemap("Speed Lines Remap", Range( 0 , 1)) = 0.8
		_SpeedLinesAnimation("Speed Lines Animation", Float) = 3
		_MaskScale("Mask Scale", Range( 0 , 2)) = 1
		_MaskHardness("Mask Hardness", Range( 0 , 1)) = 0
		_MaskPower("Mask Power", Float) = 5
		_Strength("Strength", Range( 0 , 1)) = 0.5
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "AlphaTest+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Unlit keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float _SpeedLinesRadialScale;
		uniform float _SpeedLinesTiling;
		uniform float _SpeedLinesAnimation;
		uniform float _SpeedLinesPower;
		uniform float _SpeedLinesRemap;
		uniform float _Strength;
		uniform float _MaskScale;
		uniform float _MaskHardness;
		uniform float _MaskPower;
		uniform float4 _Colour;
		uniform float _Cutoff = 0.5;


		float3 mod2D289( float3 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float2 mod2D289( float2 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float3 permute( float3 x ) { return mod2D289( ( ( x * 34.0 ) + 1.0 ) * x ); }

		float snoise( float2 v )
		{
			const float4 C = float4( 0.211324865405187, 0.366025403784439, -0.577350269189626, 0.024390243902439 );
			float2 i = floor( v + dot( v, C.yy ) );
			float2 x0 = v - i + dot( i, C.xx );
			float2 i1;
			i1 = ( x0.x > x0.y ) ? float2( 1.0, 0.0 ) : float2( 0.0, 1.0 );
			float4 x12 = x0.xyxy + C.xxzz;
			x12.xy -= i1;
			i = mod2D289( i );
			float3 p = permute( permute( i.y + float3( 0.0, i1.y, 1.0 ) ) + i.x + float3( 0.0, i1.x, 1.0 ) );
			float3 m = max( 0.5 - float3( dot( x0, x0 ), dot( x12.xy, x12.xy ), dot( x12.zw, x12.zw ) ), 0.0 );
			m = m * m;
			m = m * m;
			float3 x = 2.0 * frac( p * C.www ) - 1.0;
			float3 h = abs( x ) - 0.5;
			float3 ox = floor( x + 0.5 );
			float3 a0 = x - ox;
			m *= 1.79284291400159 - 0.85373472095314 * ( a0 * a0 + h * h );
			float3 g;
			g.x = a0.x * x0.x + h.x * x0.y;
			g.yz = a0.yz * x12.xz + h.yz * x12.yw;
			return 130.0 * dot( m, g );
		}


		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float2 CenteredUV15_g1 = ( i.uv_texcoord - float2( 0.5,0.5 ) );
			float2 break17_g1 = CenteredUV15_g1;
			float2 appendResult23_g1 = (float2(( length( CenteredUV15_g1 ) * _SpeedLinesRadialScale * 2.0 ) , ( atan2( break17_g1.x , break17_g1.y ) * ( 1.0 / 6.28318548202515 ) * _SpeedLinesTiling )));
			float2 appendResult58 = (float2(( -_SpeedLinesAnimation * _Time.y ) , 0.0));
			float simplePerlin2D10 = snoise( ( appendResult23_g1 + appendResult58 ) );
			simplePerlin2D10 = simplePerlin2D10*0.5 + 0.5;
			float temp_output_1_0_g6 = _SpeedLinesRemap;
			float SpeedLines21 = saturate( ( ( pow( simplePerlin2D10 , _SpeedLinesPower ) - temp_output_1_0_g6 ) / ( 1.0 - temp_output_1_0_g6 ) ) );
			float2 uv_TexCoord60 = i.uv_texcoord * float2( 2,2 ) + float2( -1,-1 );
			float temp_output_84_0 = (2.0 + (_Strength - -0.3) * (0.0 - 2.0) / (1.0 - -0.3));
			float temp_output_1_0_g5 = ( temp_output_84_0 * _MaskScale );
			float lerpResult71 = lerp( 0.0 , ( temp_output_84_0 * _MaskScale ) , _MaskHardness);
			float Mask24 = pow( ( 1.0 - saturate( ( ( length( uv_TexCoord60 ) - temp_output_1_0_g5 ) / ( ( lerpResult71 - 0.001 ) - temp_output_1_0_g5 ) ) ) ) , _MaskPower );
			float MaskedSpeedLines29 = ( SpeedLines21 * Mask24 );
			float3 ColourRGB38 = (_Colour).rgb;
			o.Emission = ( MaskedSpeedLines29 * ColourRGB38 );
			o.Alpha = 1;
			float ColourA40 = _Colour.a;
			clip( ( MaskedSpeedLines29 * ColourA40 ) - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18935
1490;73;1408;813;4031.919;418.1457;1.007637;True;False
Node;AmplifyShaderEditor.RangedFloatNode;55;-3801.88,-316.5227;Inherit;False;Property;_SpeedLinesAnimation;Speed Lines Animation;6;0;Create;True;0;0;0;False;0;False;3;3;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;82;-3753.141,-133.7993;Inherit;False;Property;_Strength;Strength;10;0;Create;True;0;0;0;False;0;False;0.5;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;84;-3416.25,-140.0378;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;-0.3;False;2;FLOAT;1;False;3;FLOAT;2;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.NegateNode;59;-3556.806,-310.7944;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;57;-3620.437,-217.8602;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;17;-3715.34,-35.61016;Inherit;False;Property;_MaskScale;Mask Scale;7;0;Create;True;0;0;0;False;0;False;1;1;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;18;-3580.788,379.4019;Inherit;False;Property;_MaskHardness;Mask Hardness;8;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;81;-3260.419,33.21318;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;23;-3741.493,-463.1422;Inherit;False;Property;_SpeedLinesTiling;Speed Lines Tiling;2;0;Create;True;0;0;0;False;0;False;200;200;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;22;-3767.248,-555.6451;Inherit;False;Property;_SpeedLinesRadialScale;Speed Lines Radial Scale;3;0;Create;True;0;0;0;False;0;False;0.1;0.1;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;56;-3382.927,-307.826;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;11;-3423.667,-571.0438;Inherit;True;Polar Coordinates;-1;;1;7dab8e02884cf104ebefaa2e788e4162;0;4;1;FLOAT2;0,0;False;2;FLOAT2;0.5,0.5;False;3;FLOAT;0.38;False;4;FLOAT;28.02;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;58;-3212.592,-310.2251;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;60;-4059.914,75.28694;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;2,2;False;1;FLOAT2;-1,-1;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;71;-3091.972,191.2075;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LengthOpNode;62;-3613.054,81.07972;Inherit;True;1;0;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;80;-3069.991,-93.6161;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;70;-2928.311,191.2073;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0.001;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;54;-2999.59,-545.3854;Inherit;True;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;51;-2683.448,-295.9243;Inherit;False;Property;_SpeedLinesPower;Speed Lines Power;4;0;Create;True;0;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;10;-2682.032,-544.4982;Inherit;True;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;63;-2727.063,37.7415;Inherit;True;Inverse Lerp;-1;;5;09cbe79402f023141a4dc1fddd4c9511;0;3;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;53;-2349.465,-576.2127;Inherit;False;Property;_SpeedLinesRemap;Speed Lines Remap;5;0;Create;True;0;0;0;False;0;False;0.8;0.8;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;48;-2311.938,-356.86;Inherit;True;False;2;0;FLOAT;0;False;1;FLOAT;5.23;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;68;-2448.041,37.9687;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;14;-2274.064,37.82519;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;25;-2265.914,271.0869;Inherit;False;Property;_MaskPower;Mask Power;9;0;Create;True;0;0;0;False;0;False;5;5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;50;-1987.354,-541.1801;Inherit;True;Inverse Lerp;-1;;6;09cbe79402f023141a4dc1fddd4c9511;0;3;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;16;-2005.64,38.10326;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;3.91;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;52;-1659.837,-483.6926;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;24;-1757.859,33.23505;Inherit;False;Mask;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;21;-1481.31,-489.1878;Inherit;False;SpeedLines;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;26;-2641.187,429.7707;Inherit;False;21;SpeedLines;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;27;-2624.943,647.835;Inherit;False;24;Mask;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;33;-2736.949,911.6467;Inherit;False;Property;_Colour;Colour;1;1;[HDR];Create;True;0;0;0;False;0;False;1,1,1,1;1,1,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;15;-2307.729,573.4354;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;43;-2491.353,914.8679;Inherit;False;True;True;True;False;1;0;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;38;-2013.18,898.6451;Inherit;False;ColourRGB;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;29;-2054.516,569.8716;Inherit;False;MaskedSpeedLines;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;40;-2454.421,1150.243;Inherit;False;ColourA;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;36;-1041.189,-171.0469;Inherit;False;29;MaskedSpeedLines;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;46;-1060.045,-488.7715;Inherit;False;38;ColourRGB;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;76;-927.5978,54.62134;Inherit;False;40;ColourA;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;45;-716.8903,-473.545;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;79;-635.6704,-63.3884;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;75;-353.1531,-393.6891;Float;False;True;-1;2;ASEMaterialInspector;0;0;Unlit;James/speedLines;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Masked;0.5;True;True;0;False;TransparentCutout;;AlphaTest;All;18;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;84;0;82;0
WireConnection;59;0;55;0
WireConnection;81;0;84;0
WireConnection;81;1;17;0
WireConnection;56;0;59;0
WireConnection;56;1;57;0
WireConnection;11;3;22;0
WireConnection;11;4;23;0
WireConnection;58;0;56;0
WireConnection;71;1;81;0
WireConnection;71;2;18;0
WireConnection;62;0;60;0
WireConnection;80;0;84;0
WireConnection;80;1;17;0
WireConnection;70;0;71;0
WireConnection;54;0;11;0
WireConnection;54;1;58;0
WireConnection;10;0;54;0
WireConnection;63;1;80;0
WireConnection;63;2;70;0
WireConnection;63;3;62;0
WireConnection;48;0;10;0
WireConnection;48;1;51;0
WireConnection;68;0;63;0
WireConnection;14;0;68;0
WireConnection;50;1;53;0
WireConnection;50;3;48;0
WireConnection;16;0;14;0
WireConnection;16;1;25;0
WireConnection;52;0;50;0
WireConnection;24;0;16;0
WireConnection;21;0;52;0
WireConnection;15;0;26;0
WireConnection;15;1;27;0
WireConnection;43;0;33;0
WireConnection;38;0;43;0
WireConnection;29;0;15;0
WireConnection;40;0;33;4
WireConnection;45;0;36;0
WireConnection;45;1;46;0
WireConnection;79;0;36;0
WireConnection;79;1;76;0
WireConnection;75;2;45;0
WireConnection;75;10;79;0
ASEEND*/
//CHKSM=59FDECF0E258120F43A9F1E611A997EBFD6BA3D7