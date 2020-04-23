using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    private enum Color
    {
        Red, Orange, Yellow, Olive, Green, Blue, Magenta, Purple, Pink
    }

    private enum Name
    {
        // Water Names
        Bluey, Bubbles, Gills, Iris, Pearl,
        // Fire Names
        Ashes, Bandit, Blaze, Gareth, Toast,
        // Air Names
        Candy, Cece, Cosmo, Cupcake, Pixie,
        // Earth Names
        Acorn, Coco, Jerald, Peanut, Rango,
        // Electric Names
        Amp, Happy, Lightning, Spark, Stormy
    }

    public GameObject enemy;
    public static List<GameObject> enemies = new List<GameObject>();

    public Vector3 playerPosition;
    private List<Transform> spawnPositions = new List<Transform>();
    private static int listPos = 0;

    private class ShuffleComparer<T> : IComparer<T>
    {
        public int Compare(T x, T y)
        {
            return Random.Range(-1, 1);
        }
    }

    private void Start()
    {
        enemies.Clear();
        listPos = 0;

        playerPosition = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().lastPlayerPos;

        foreach (GameObject pos in GameObject.FindGameObjectsWithTag("SpawnLocation"))
        {
            if (Vector3.Distance(pos.transform.position, playerPosition) > 10f)
                spawnPositions.Add(pos.transform);
        }
        spawnPositions.Sort(0, spawnPositions.Count, new ShuffleComparer<Transform>());

        for (int i = 0; i < 6; i++)
        {
            Color enemyColor = RandomColor();
            Vector3 position = RandomPosition();
            int enemyHP = Random.Range(100, 150);
            int hunger = Random.Range(100, 125);
            int damage = Random.Range(15, 35);
            int gold = Random.Range(20, 40);
            int sourAff = Random.Range(1, 5);
            int spicyAff = Random.Range(1, 5);
            int seafoodAff = Random.Range(1, 5);
            int candyAff = Random.Range(1, 5);
            int veggieAff = Random.Range(1, 5);

            SpawnEnemy(position, enemyColor, enemyHP, hunger, enemyHP, damage, gold, seafoodAff, spicyAff, candyAff, veggieAff, sourAff);
        }
    }

    private void SpawnEnemy(Vector3 position, Color color, int maxHP, int hunger, int currentHP, int dmg, int goldReward, int waterAff, int fireAff, int airAff, int earthAff, int electricAff)
    {
        // Sets components.
        GameObject newEnemy = Instantiate(enemy, position, Quaternion.identity);
        Player stats = newEnemy.GetComponent<Player>();
        Transform enemyTrans = newEnemy.GetComponent<Transform>();
        newEnemy.GetComponent<EnemySlimeMove>().enabled = true;

        // Sets stats based on arguments.
        stats.color = color.ToString();
        stats.health = maxHP;
        stats.hunger = hunger;
        stats.currentHP = stats.health;
        stats.dmg = dmg;
        stats.VicGold = goldReward;
        stats.sourAff = electricAff;
        stats.spicyAff = fireAff;
        stats.seafoodAff = waterAff;
        stats.candyAff = airAff;
        stats.veggieAff = earthAff;

        // Determines the size of the slime based on an average of stats.
        stats.size = (( (float) maxHP / 25 ) + ( (float) dmg / 5 ) + (( (float) waterAff + (float) fireAff + (float) airAff + (float) earthAff + (float) electricAff ) / 5) / 7) / 9;
        enemyTrans.localScale = new Vector3(stats.size, stats.size, stats.size);

        // Changes type based on highest affinity.
        List<int> affinities = new List<int>();

        affinities.Add(waterAff);
        affinities.Add(fireAff);
        affinities.Add(airAff);
        affinities.Add(earthAff);
        affinities.Add(electricAff);

        affinities.Sort();
        affinities.Reverse();
        string typeString;

        if (affinities[0] == electricAff)
            typeString = "Electric";
        else if (affinities[0] == fireAff)
            typeString = "Fire";
        else if (affinities[0] == waterAff)
            typeString = "Water";
        else if (affinities[0] == airAff)
            typeString = "Air";
        else
            typeString = "Earth";

        stats.type = typeString;

        // Sets a random name based on type.
        stats.name = RandomName(typeString);
        enemies.Add(newEnemy);
    }

    private Color RandomColor()
    {
        int colorCount = 9;
        int randomColor = Random.Range(0, colorCount);

        return (Color) randomColor;
    }

    private string RandomName(string type)
    {
        int randomName;
        switch (type)
        {
            case "Water Type":
                randomName = Random.Range(0, 5);
                break;
            case "Fire Type":
                randomName = Random.Range(5, 10);
                break;
            case "Air Type":
                randomName = Random.Range(10, 15);
                break;
            case "Earth Type":
                randomName = Random.Range(15, 20);
                break;
            case "Electric Type":
                randomName = Random.Range(20, 25);
                break;
            default:
                randomName = Random.Range(0,25);
                break;
        }

        return ((Name) randomName).ToString();
    }

    private Vector3 RandomPosition()
    {
        return spawnPositions[listPos++].position;
    }
}
