Shader "Unlit/Depth"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _DepthColor("Depth Color", Color) = (1.0, 1.0, 1.0, 1.0)
        _DepthPower("Depth Power", Range(1.0, 1000.0)) = 1.0
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma shader_feature _ ON_RENDER_SCENE_VIEW

            #include "UnityCG.cginc"

            #ifdef ON_RENDER_SCENE_VIEW
                #define IS_SCENE_VIEW 1
            #else
                #define IS_SCENE_VIEW 0
            #endif

            sampler2D _MainTex;
            float4 _MainTex_ST, _DepthColor;;
            float _DepthPower;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 view : ANY;
            };

            v2f vert(appdata v)
            {
                v2f o;

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                o.view = UnityObjectToViewPos(v.vertex);

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                return IS_SCENE_VIEW == 0 || (IS_SCENE_VIEW == 1 && i.view.x >= 0.5)?
                    fixed4(lerp(col, _DepthColor, saturate(i.view.z / (-1.0 * _DepthPower))).rgb, 1.0) :
                    col;
            }
            ENDCG
        }
    }
}
