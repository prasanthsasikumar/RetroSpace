Shader "Custom/SkyMaskShader"
{
    SubShader
    {
        // Make sure it's rendered after opaque objects
        Tags { "RenderType"="Opaque" }

        // Disable depth writing
        ZWrite Off
        ZTest Always

        // Add stencil operations
        Pass
        {
            Stencil
            {
                Ref 1       // Reference value for stencil buffer
                Comp always // Always pass stencil comparison
                Pass replace // Replace the stencil buffer value with Ref
            }

            // Optional: You can choose to have no color output with this
            ColorMask 0 // Don't render any colors (renders skybox only)
        }
    }
}
