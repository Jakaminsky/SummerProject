using UnityEngine;

public class Orbital : MonoBehaviour
{
    public float rotationSpeed = 5.0f;

    public GameObject projectile;

    private GameObject projectileOne;
    public Material fireMaterial;

    private GameObject projectileTwo;
    public Material iceMaterial;

    private GameObject projectileThree;
    public Material poisonMaterial;

    private GameObject projectileFour;
    public Material explosiveMaterial;

    private void Start()
    {
        createProjectiles();
    }

    private void Update()
    {
        projectileOne.transform.RotateAround(gameObject.transform.position, Vector3.up, rotationSpeed * Time.deltaTime);
        projectileTwo.transform.RotateAround(gameObject.transform.position, Vector3.up, rotationSpeed * Time.deltaTime);
        projectileThree.transform.RotateAround(gameObject.transform.position, Vector3.up, rotationSpeed * Time.deltaTime);
        projectileFour.transform.RotateAround(gameObject.transform.position, Vector3.up, rotationSpeed * Time.deltaTime);
    }

    private void createProjectiles()
    {
        projectileOne = Instantiate(projectile, new Vector3(gameObject.transform.position.x + 5, gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity);
        projectileOne.transform.parent = transform;
        projectileOne.transform.rotation = Quaternion.Euler(0, 0, 90f);
        projectileOne.GetComponent<MeshRenderer>().material = fireMaterial;
        projectileOne.GetComponent<OrbitElements>().orbitdamageType = DamageType.Fire;

        projectileTwo = Instantiate(projectile, new Vector3(gameObject.transform.position.x - 5, gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity);
        projectileTwo.transform.parent = transform;
        projectileTwo.transform.rotation = Quaternion.Euler(0, 0, -90f);
        projectileTwo.GetComponent<MeshRenderer>().material = iceMaterial;
        projectileTwo.GetComponent<OrbitElements>().orbitdamageType = DamageType.Ice;

        projectileThree = Instantiate(projectile, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 5), Quaternion.identity);
        projectileThree.transform.parent = transform;
        projectileThree.transform.rotation = Quaternion.Euler(90f, 0, 0);
        projectileThree.GetComponent<MeshRenderer>().material = poisonMaterial;
        projectileThree.GetComponent<OrbitElements>().orbitdamageType = DamageType.Poison;

        projectileFour = Instantiate(projectile, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - 5), Quaternion.identity);
        projectileFour.transform.parent = transform;
        projectileFour.transform.rotation = Quaternion.Euler(90f, 0, 0);
        projectileFour.GetComponent<MeshRenderer>().material = explosiveMaterial;
        projectileFour.GetComponent<OrbitElements>().orbitdamageType = DamageType.Explosive;
    }

}
