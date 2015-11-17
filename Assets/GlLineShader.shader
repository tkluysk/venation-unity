Shader "Lines/Colored Blended" { 
	Properties { _Color ("Main Color", Color) = (1,1,1,0.5) } 

	SubShader { 
		Tags {"Queue" = "Transparent" "RenderType" = "Transparent" } 
		Pass { 
			BindChannels { Bind "Color",color } 
			Blend SrcAlpha OneMinusSrcAlpha 
			ZWrite Off 
			Cull Off 
			Lighting Off
			Fog { Mode Off } 
			Color[_Color] 

		}
	}
}
