// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Mobile/MobileWaterShaderEKV_v2" {

    Properties{
        [Toggle(LIGHTING)] _SECTIONLIGHTING("---- LIGHTING ------------------------", Float) = 1
        [PowerSlider(5.0)] _Shininess("Shininess", Range(0.01, 2)) = 0
        [PowerSlider(5.0)] _Brightness("Brightness", Range(0.01, 100)) = 0
        [PowerSlider(5.0)] _Attenuation("Attenuation", Range(0.001, 1)) = 0
        _Color("Color Tint",COLOR) = (0.5,0.5,0.5,1.0)

        [Toggle(TEXTURES)]_SECTIONTEXTURES("---- TEXTURES ------------------------", Float) = 1
        _MainTex("Texture A", 2D) = "black" {}
        _MainTexRot("Texture A Rotation", Range(0 , 360)) = 0

        _DiffTex("Texture B", 2D) = "black" {}
        _DiffTexRot("Texture B Rotation", Range(0 , 360)) = 0

        [Toggle(WAVES)] _SECTIONWAVES("---- WAVES AND FLOW ------------------------", Float) = 1
        [NoScaleOffset] _DerivHeightMap("Wave Height Map", 2D) = "black" {}
        _Tiling("Tiling", Float) = 3
        [PowerSlider(1.0)] _Speed("Speed", Range(0.001, 1)) = 0.1
        [PowerSlider(1.0)] _FlowStrength("Flow Strength", Range(-1, 1)) = 0.1
        [PowerSlider(1.0)] _FlowOffset("Flow Offset", Range(-1, 1)) = 0.25
        [PowerSlider(1.0)] _HeightScale("Height Scale, Constant", Range(-1, 1)) = 0.5
        [PowerSlider(1.0)] _HeightScaleModulated("Height Scale, Modulated", Range(-5, 5)) = 4

        [Toggle(FOAM)] _FOAM("---- EDGE FOAM ------------------------", Float) = 1
        _FoamTex("Foam Texture", 2D) = "white" {}
        _FoamSpeed("Foam Speed", Range(0, 1)) = 0.1
        _FoamIntensity("Foam Intensity", Range(0, 10)) = 4.5
        _FoamWidth("Foam width", Range(0, 1)) = 0.05
        _FoamDir("Foam strength", Range(-1, 1)) = 0.05
        _FoamTexRot("Foam Rotation", Range(0 , 360)) = 0
        [Toggle(INVERT)] _Invert("Show foam on center (offset center using foam texture offset). Disable to show foam on edges instead", Float) = 1
        [Toggle(SQUARED)] _Squared("Show foam as a square instead of circle", Float) = 1

        [Toggle(REFLECTION)] _Reflection("---- CUBEMAP REFLECTION ------------------------", Float) = 1
        [Slider] _RefStrength("Reflection Strength", Range(0, 2)) = 1
        _Cube("Cubemap", CUBE) = "" {}

        [Toggle(RENDERING)] _Rendering("---- BLENDING MODE (always on) ------------------------", Float) = 1
        SrcMode("Src Mode - recommended: 4 (opaque), 5 (transparent)", Float) = 4
        DstMode("Dst Mode - recommended: 3", Float) = 3
        [Toggle(COMMTRANSP)] _COMMTRANSP("(Remember setting render queue to 3000 for transparency)", Float) = 1

    }


        /////////////////////////////////// LOD 600

            SubShader{


                Tags { "RenderType" = "Opaque" "RenderQueue" = "Transparent"}
                LOD 600

                Blend[SrcMode][DstMode]

            CGPROGRAM

            #pragma shader_feature LIGHTING
            #pragma shader_feature TEXTURES
            #pragma shader_feature WAVES
            #pragma shader_feature REFLECTION
            #pragma shader_feature FOAM
            #pragma shader_feature INVERT
            #pragma shader_feature SQUARED
            #pragma shader_feature RENDERING
            #pragma surface surf MobileBlinnPhong fullforwardshadows exclude_path:prepass nolightmap noforwardadd halfasview interpolateview
            #pragma target 3.5

             fixed4 _Color;
             float4 tint0;
             float _Brightness;
             float _RefStrength;
             float _Attenuation;
             float _MainTexRot;
             float _DiffTexRot;
             float _FoamTexRot;

             sampler2D _DerivHeightMap;
             float _Speed, _FlowStrength, _FlowOffset, _Tiling;
             float _HeightScale, _HeightScaleModulated;

             sampler2D _MainTex;
             sampler2D _DiffTex;
             samplerCUBE _Cube;
             half _Shininess;

             sampler2D _FoamTex;
             half _FoamSpeed;
             half _FoamIntensity;
             half _FoamWidth;
             half _FoamDir;
             half _FoamSides;

             struct Input {
                 float2 uv_MainTex;
                 float2 uv_DiffTex;
                 float2 uv_FoamTex;
                 float3 worldRefl;
                 float4 screenPos;
                 float4 pos : SV_POSITION;
                 INTERNAL_DATA
             };

             float3 FlowUVW(
                 float2 uv, float2 flowVector, float2 jump,
                 float flowOffset, float tiling, float time, bool flowB
             ) {
                 float phaseOffset = flowB ? 0.5 : 0;
                 float progress = frac(time + phaseOffset);
                 float3 uvw;
                 uvw.xy = uv - flowVector * (progress + flowOffset);
                 uvw.xy *= tiling;
                 uvw.xy += phaseOffset;
                 uvw.xy += (time - progress) * jump;
                 uvw.z = 1 - abs(1 - 2 * progress);
                 return uvw;
             }

             void Unity_Rotate_Degrees_float(float2 UV, float2 Center, float Rotation, out float2 Out)
             {
                 Rotation = Rotation * (3.1415926f / 180.0f);
                 UV -= Center;
                 float s = sin(Rotation);
                 float c = cos(Rotation);
                 float2x2 rMatrix = float2x2(c, -s, s, c);
                 rMatrix *= 0.5;
                 rMatrix += 0.5;
                 rMatrix = rMatrix * 2 - 1;
                 UV.xy = mul(UV.xy, rMatrix);
                 UV += Center;
                 Out = UV;
             }

             inline fixed4 LightingMobileBlinnPhong(SurfaceOutput s, fixed3 lightDir, fixed3 halfDir, fixed atten)
             {
                 fixed diff = max(0, dot(s.Normal, lightDir));
                 fixed nh = max(0, dot(s.Normal, halfDir));
                 fixed spec = pow(nh, s.Specular * 128) * s.Gloss;

                 fixed4 c;
     #ifdef LIGHTING
                 c.rgb = (s.Albedo * _LightColor0.rgb * diff + (_LightColor0.rgb * _Brightness) * spec) * _Attenuation;
     #else
                 c.rgb = (s.Albedo * _LightColor0.rgb * diff * spec) * atten;
     #endif

                 UNITY_OPAQUE_ALPHA(c.a);

                 return c;
             }

             float3 UnpackDerivativeHeight(float4 textureData) {
                 float3 dh = textureData.agb;
                 dh.xy = dh.xy * 2 - 1;
                 return dh;
             }


             void surf(Input IN, inout SurfaceOutput o) {

                 //texture rotation
                 Unity_Rotate_Degrees_float(IN.uv_MainTex, 0, _MainTexRot, IN.uv_MainTex);
                 Unity_Rotate_Degrees_float(IN.uv_DiffTex, 0, _DiffTexRot, IN.uv_DiffTex);
                 

                 fixed4 tex = tex2D(_MainTex, IN.uv_MainTex) * _Color;
                 fixed4 tex2 = tex2D(_DiffTex, IN.uv_DiffTex) * _Color;

                 float finalHeightScale = 0;
     
                 float noise = 0;
                 float time = 0;
#ifdef WAVES
                 //flow
                 float3 flow = tex2D(_DiffTex, IN.uv_MainTex).rgb;
                 flow.xy = flow.xy * 2 - 1;
                 flow *= _FlowStrength;
                 noise = tex2D(_DiffTex, IN.uv_MainTex).a;
                 time = _Time.y * _Speed + noise;


                 float3 uvwA = FlowUVW(
                     IN.uv_MainTex, flow.xy, 0,
                     _FlowOffset, _Tiling, time, false
                 );
                 float3 uvwB = FlowUVW(
                     IN.uv_MainTex, flow.xy, 0,
                     _FlowOffset, _Tiling, time, true
                 );


                 finalHeightScale =
                     flow.z * _HeightScaleModulated + _HeightScale;

                 float3 dhA =
                     UnpackDerivativeHeight(tex2D(_DerivHeightMap, uvwA.xy)) *
                     (uvwA.z * finalHeightScale);
                 float3 dhB =
                     UnpackDerivativeHeight(tex2D(_DerivHeightMap, uvwB.xy)) *
                     (uvwB.z * finalHeightScale);
                 o.Normal = normalize(float3(-(dhA.xy + dhB.xy), 2));

                 //end flow
     #endif

#ifdef TEXTURES
                 o.Albedo = tex.rgb + tex2.rgb;
#endif
                 o.Gloss = tex.a + tex2.a;
                 o.Alpha = tex.a + tex2.a;
     #ifdef LIGHTING
                 o.Specular = 1 / _Shininess;
     #else
                 o.Specular = 1;
     #endif


                //reflection
                #ifdef REFLECTION
                    o.Albedo = o.Albedo + ((texCUBE(_Cube, WorldReflectionVector(IN, o.Normal)).rgb * _Color) * _RefStrength);
                #endif

                //foam
                #ifdef FOAM
                    // Calculate the center of the circle (you may replace these values with your desired center)
                    half2 circleCenter = float2(0.5, 0.5);

                    // Calculate the distance from the current pixel to the center of the circle
                    half distanceToCenter = length(IN.uv_FoamTex - circleCenter);
                    

                    // Define the radius of the circular mask
                    half circleRadius = 1; // You can replace this with your desired radius value

                    half foamMask = 1;
#ifdef SQUARED
    #ifdef INVERT
                    float2 uvFromCenter = abs(IN.uv_FoamTex - circleCenter);
                    // Create the square mask based on the distance from the center
                    half foamMaskX = 1 - smoothstep(0.0, _FoamWidth, uvFromCenter.x);
                    half foamMaskY = 1 - smoothstep(0.0, _FoamWidth, uvFromCenter.y);
                    foamMask = min(foamMaskX, foamMaskY);

    #else
                    half foamMask1 = 1 - smoothstep(0.0, _FoamWidth, IN.uv_FoamTex.x) * smoothstep(0.0, _FoamWidth, IN.uv_FoamTex.y);
                    half foamMask2 = 1 - smoothstep(1.0, 1 - _FoamWidth, IN.uv_FoamTex.y) * smoothstep(1.0, 1 - _FoamWidth, IN.uv_FoamTex.x);
                    foamMask = foamMask1 + foamMask2;
    #endif
                    
#else

                    // Calculate the circular mask
                    #ifdef INVERT
                    foamMask = 1 - smoothstep(0.0, _FoamWidth, distanceToCenter);
                    #else
                    foamMask = smoothstep(circleRadius - _FoamWidth, circleRadius, distanceToCenter);
                    #endif
    
#endif
                    // Calculate foam mask for the edges (squared)


                    // Foam texture with tiling and offset
                    half2 foamUV = IN.uv_MainTex;
                    //foamUV += _Time.y * _FoamSpeed * _FoamDir; // Add time-based offset for animation (single direction)
                    Unity_Rotate_Degrees_float(foamUV, circleCenter, _FoamTexRot, foamUV);

                    //Center-oriented:
                    // Calculate the direction from the current pixel to the circle center
                    half2 toCenter = circleCenter - IN.uv_FoamTex;
                    // Normalize the direction vector
                    toCenter /= distanceToCenter;
                    // Calculate the foamUV offset based on the direction towards the center
                    
                    noise = tex2D(_FoamTex, foamUV).a;
                    time = _Time.y * _Speed + noise;
                    float3 uvF = FlowUVW(
                        foamUV, toCenter, 0,
                        0, 0, time, true
                    );

                    foamUV += (_Time.y * _FoamSpeed) * _FoamDir;

                    half4 foamColor = tex2D(_FoamTex, foamUV);

                    // Combine water and foam
#ifdef TEXTURES
                    half4 finalColor = lerp(tex, tex * _FoamIntensity, foamMask);
#else      
                    half4 finalColor = lerp(_Color, _Color * _FoamIntensity, foamMask );
#endif
                    half finalFoamMask = (foamMask * _FoamIntensity);

                    finalColor.rgba += foamColor.rgba * finalFoamMask;

                    o.Albedo += finalColor;
                #endif

                    }

                    ENDCG

        }


            FallBack "Mobile/Diffuse"
}
