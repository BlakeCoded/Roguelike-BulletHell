Shader "Custom/GreyboxTiles"
{
    Properties
    {
        _ColorA ("Tile Color A", Color) = (0.55,0.55,0.55,1)
        _ColorB ("Tile Color B", Color) = (0.45,0.45,0.45,1)

        //_LineColor ("Line Color", Color) = (0.2,0.2,0.2,1)

        _TileSize ("Tile Size", Float) = 8
        //_LineWidth ("Line Width", Range(0.001, 0.2)) = 0.03
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            fixed4 _ColorA;
            fixed4 _ColorB;
            //fixed4 _LineColor;

            float _TileSize;
            //float _LineWidth;

            v2f vert(appdata v)
            {
                v2f o;

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv * _TileSize;

                return o;
            }

            // fixed4 frag(v2f i) : SV_Target
            // {
            //     float2 tile = floor(i.uv);

            //     // Checker pattern
            //     float checker = fmod(tile.x + tile.y, 2.0);

            //     fixed4 baseColor = lerp(_ColorA, _ColorB, checker);

            //     // Grid lines
            //     float2 gridUV = frac(i.uv);

            //     float lineX =
            //         step(gridUV.x, _LineWidth) +
            //         step(1.0 - gridUV.x, _LineWidth);

            //     float lineY =
            //         step(gridUV.y, _LineWidth) +
            //         step(1.0 - gridUV.y, _LineWidth);

            //     float grid = saturate(lineX + lineY);

            //     return lerp(baseColor, _LineColor, grid);
            // }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 tile = floor(i.uv);

                // Checkerboard pattern
                float checker = fmod(tile.x + tile.y, 2.0);

                return lerp(_ColorA, _ColorB, checker);
            }

            ENDCG
        }
    }

    FallBack "Diffuse"
}