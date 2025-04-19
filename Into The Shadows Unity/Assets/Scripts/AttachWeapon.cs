using UnityEngine;

public class AttachWeapon : MonoBehaviour
{
    public GameObject weaponPrefab;

    void Start()
    {
        Animator animator = GetComponent<Animator>();
        Transform rightHand = animator.GetBoneTransform(HumanBodyBones.RightHand);

        if (rightHand != null && weaponPrefab != null)
        {
            GameObject weaponInstance = Instantiate(weaponPrefab, rightHand);
            weaponInstance.transform.localPosition = new Vector3(-0.3238f, 0.0977f, 0.0629f);
            weaponInstance.transform.localRotation = Quaternion.Euler(0.454f, 185.672f, 1.832f);
        }
    }
}
