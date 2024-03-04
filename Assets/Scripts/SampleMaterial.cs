using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// MaterialPropertyBlockを用いて、オブジェクトを半透明化にする
/// </summary>
public class SampleMaterial : MonoBehaviour
{
    // 親子オブジェクトを格納
    MeshRenderer[] meshRenderers;
    // オブジェクトの初期色
    Color color = Color.white;

    MaterialPropertyBlock propertyBlock;

    /// <summary>
    /// MaterialPropertyBlockを初期化する
    /// </summary>
    public MaterialPropertyBlock materialPropertyBlock
    {
        get
        {
            return propertyBlock ?? (propertyBlock = new MaterialPropertyBlock());
        }
    }

    private void Awake()
    {
        meshRenderers = this.GetComponentsInChildren<MeshRenderer>();
    }

    /// <summary>
    /// マテリアルを半透明にする
    /// </summary>
    public void ClearMaterialInvoke()
    {
        // アルファ値の設定
        color.a = 0.15f;
        // 設定したアルファ値をMaterialPropertyBlockに反映
        propertyBlock.SetColor(Shader.PropertyToID("_Color"), color);
        // 取得したすべてのMaterialPropertyBlockにpropertyBlockを適応する
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            meshRenderers[i].GetComponent<Renderer>().material.shader = Shader.Find("Transparent/Diffuse ZWrite");
            meshRenderers[i].SetPropertyBlock(materialPropertyBlock);
        }
    }

    /// <summary>
    /// マテリアルを不透明にする
    /// </summary>
    public void NotClearMaterialInvoke()
    {
        // アルファ値の設定
        color.a = 1.0f;
        // 設定したアルファ値をMaterialPropertyBlockに反映
        propertyBlock.SetColor(Shader.PropertyToID("_Color"), color);
        // 取得したすべてのMaterialPropertyBlockにpropertyBlockを適応する
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            meshRenderers[i].GetComponent<Renderer>().material.shader = Shader.Find("Mobile/Diffuse");
            meshRenderers[i].SetPropertyBlock(materialPropertyBlock);
        }
    }
}
