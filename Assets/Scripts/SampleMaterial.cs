using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// MaterialPropertyBlock��p���āA�I�u�W�F�N�g�𔼓������ɂ���
/// </summary>
public class SampleMaterial : MonoBehaviour
{
    // �e�q�I�u�W�F�N�g���i�[
    MeshRenderer[] meshRenderers;
    // �I�u�W�F�N�g�̏����F
    Color color = Color.white;

    MaterialPropertyBlock propertyBlock;

    /// <summary>
    /// MaterialPropertyBlock������������
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
    /// �}�e���A���𔼓����ɂ���
    /// </summary>
    public void ClearMaterialInvoke()
    {
        // �A���t�@�l�̐ݒ�
        color.a = 0.15f;
        // �ݒ肵���A���t�@�l��MaterialPropertyBlock�ɔ��f
        propertyBlock.SetColor(Shader.PropertyToID("_Color"), color);
        // �擾�������ׂĂ�MaterialPropertyBlock��propertyBlock��K������
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            meshRenderers[i].GetComponent<Renderer>().material.shader = Shader.Find("Transparent/Diffuse ZWrite");
            meshRenderers[i].SetPropertyBlock(materialPropertyBlock);
        }
    }

    /// <summary>
    /// �}�e���A����s�����ɂ���
    /// </summary>
    public void NotClearMaterialInvoke()
    {
        // �A���t�@�l�̐ݒ�
        color.a = 1.0f;
        // �ݒ肵���A���t�@�l��MaterialPropertyBlock�ɔ��f
        propertyBlock.SetColor(Shader.PropertyToID("_Color"), color);
        // �擾�������ׂĂ�MaterialPropertyBlock��propertyBlock��K������
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            meshRenderers[i].GetComponent<Renderer>().material.shader = Shader.Find("Mobile/Diffuse");
            meshRenderers[i].SetPropertyBlock(materialPropertyBlock);
        }
    }
}
