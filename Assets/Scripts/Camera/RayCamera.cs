using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// カメラとターゲットの間にRayを飛ばし、
/// 指定のオブジェクトに当たったらそのオブジェクトを半透明にする
/// </summary>
public class RayCamera : MonoBehaviour
{
    // カメラのターゲット
    [SerializeField]
    Transform target;
    // 半透明にしたいオブジェクトのタグ
    public string TransparentObjectTagName;

    // 前フレームで遮蔽物として扱われたオブジェクトを格納
    [SerializeField]
    GameObject[] prevRaycast;
    // 不透明にしたオブジェクトを格納
    [SerializeField]
    List<GameObject> raycastHitsList = new List<GameObject>();

    // Update is called once per frame
    void Update()
    {
        // カメラ-target間のベクトルの取得
        Vector3 direction = target.position - this.transform.position;
        // 正規化
        direction.Normalize();

        // Rayの作成
        Ray ray = new Ray(this.transform.position, direction);
        // Rayが衝突したすべてのコライダーの情報を格納
        RaycastHit[] raycastHits = Physics.RaycastAll(ray);
        // 前フレームで弊社物となったすべてのゲームオブジェクトを保持
        prevRaycast = raycastHitsList.ToArray();
        raycastHitsList.Clear();

        foreach(RaycastHit hit in raycastHits)
        {
            SampleMaterial sampleMaterial = hit.collider.GetComponent<SampleMaterial>();

            if(hit.collider.tag == TransparentObjectTagName)
            {
                sampleMaterial.ClearMaterialInvoke();
                // 不透明にしたオブジェクトを追加
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
