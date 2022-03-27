using System.Collections;
using UnityEngine;
using TMPro;

// Behaviour of separate cubes
public class Cube : MonoBehaviour
{
    public TextMeshProUGUI Text;
    private float _showLabelForSeconds = 0.5f;

    // If players cube collide with enemy cubes then
    // player will not be the parent of collided cube
    // and the cube will not move with player
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("EnemyCube"))
    //    {
    //        transform.parent = null;
    //    }
    //}

    public void Remove()
    {
        transform.parent = null;
    }

    public void SetPositionXZ(Vector2 positionXZ)
    {
        transform.position = new Vector3(positionXZ.x, transform.position.y, positionXZ.y);
    }

    // When the cube will be far from player it will disappear 
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    // Show text for few seconds 
    public void ShowText()
    {
        StartCoroutine(ShowTextForSeconds());
    }

    IEnumerator ShowTextForSeconds()
    {
        Text.gameObject.SetActive(true);
        yield return new WaitForSeconds(_showLabelForSeconds);
        Text.gameObject.SetActive(false);

    }
}
