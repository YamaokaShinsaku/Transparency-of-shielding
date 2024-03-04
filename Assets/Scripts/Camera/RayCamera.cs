using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// �J�����ƃ^�[�Q�b�g�̊Ԃ�Ray���΂��A
/// �w��̃I�u�W�F�N�g�ɓ��������炻�̃I�u�W�F�N�g�𔼓����ɂ���
/// </summary>
public class RayCamera : MonoBehaviour
{
    // �J�����̃^�[�Q�b�g
    [SerializeField]
    Transform target;
    // �������ɂ������I�u�W�F�N�g�̃^�O
    public string TransparentObjectTagName;

    // �O�t���[���ŎՕ����Ƃ��Ĉ���ꂽ�I�u�W�F�N�g���i�[
    [SerializeField]
    GameObject[] prevRaycast;
    // �s�����ɂ����I�u�W�F�N�g���i�[
    [SerializeField]
    List<GameObject> raycastHitsList = new List<GameObject>();

    // Update is called once per frame
    void Update()
    {
        // �J����-target�Ԃ̃x�N�g���̎擾
        Vector3 direction = target.position - this.transform.position;
        // ���K��
        direction.Normalize();

        // Ray�̍쐬
        Ray ray = new Ray(this.transform.position, direction);
        // Ray���Փ˂������ׂẴR���C�_�[�̏����i�[
        RaycastHit[] raycastHits = Physics.RaycastAll(ray);
        // �O�t���[���ŕ��Е��ƂȂ������ׂẴQ�[���I�u�W�F�N�g��ێ�
        prevRaycast = raycastHitsList.ToArray();
        raycastHitsList.Clear();

        foreach(RaycastHit hit in raycastHits)
        {
            SampleMaterial sampleMaterial = hit.collider.GetComponent<SampleMaterial>();

            if(hit.collider.tag == TransparentObjectTagName)
            {
                sampleMaterial.ClearMaterialInvoke();
                // �s�����ɂ����I�u�W�F�N�g��ǉ�
                raycastHitsList.Add(hit.collider.gameObject);
            }
        }
        foreach(GameObject gameObject in prevRaycast.Except<GameObject>(raycastHitsList))
        {
            SampleMaterial sampleMaterial = gameObject.GetComponent<SampleMaterial>();

            if (gameObject != null)
            {
                sampleMaterial.ClearMaterialInvoke();

            }
        }
    }
}
