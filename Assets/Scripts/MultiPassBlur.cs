using UnityEngine;

[ExecuteInEditMode]
public class MultiPassBlur : MonoBehaviour
{
    public Material blurMaterial;
    public int passCount = 3;
    public int downsample = 1;
    public float blurSize = 1.0f;

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        int width = src.width >> downsample;
        int height = src.height >> downsample;

        RenderTexture[] buffers = new RenderTexture[2];
        buffers[0] = RenderTexture.GetTemporary(width, height, 0);
        buffers[1] = RenderTexture.GetTemporary(width, height, 0);

        Graphics.Blit(src, buffers[0]);

        for (int i = 0; i < passCount; i++)
        {
            // Apply horizontal blur
            blurMaterial.SetVector("_BlurDirection", new Vector2(1.0f, 0.0f));
            blurMaterial.SetFloat("_BlurSize", blurSize);
            Graphics.Blit(buffers[i % 2], buffers[(i + 1) % 2], blurMaterial);

            // Apply vertical blur
            blurMaterial.SetVector("_BlurDirection", new Vector2(0.0f, 1.0f));
            blurMaterial.SetFloat("_BlurSize", blurSize);
            Graphics.Blit(buffers[(i + 1) % 2], buffers[i % 2], blurMaterial);
        }

        Graphics.Blit(buffers[passCount % 2], dest);

        RenderTexture.ReleaseTemporary(buffers[0]);
        RenderTexture.ReleaseTemporary(buffers[1]);
    }
}
