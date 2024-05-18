Shader "Custom/ScanShader"
{
    Properties
    {
        _MainTex ("Base Color", 2D) = "white" {}
        _ScanColor ("Scan Color", Color) = (1, 1, 1, 1)
        _ScanSpeed ("Scan Speed", Range(0.1, 10)) = 1
    }
 
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Lighting Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
 
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
 
            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _ScanColor;
            float _ScanSpeed;
 
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }
 
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                float scanPos = i.uv.x + _Time.y * _ScanSpeed;
                float scanFactor = saturate(sin(scanPos * 10));
                fixed4 scanColor = _ScanColor * scanFactor;
                col.rgb += scanColor.rgb;
                return col;
            }
            ENDCG
        }
    }
}