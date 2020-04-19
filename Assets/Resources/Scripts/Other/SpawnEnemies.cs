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
        Bluey, Jerald, Happy
    }

    public GameObject enemy;
    private List<GameObject> enemies = new List<GameObject>();

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
        listPos = 0;

        foreach (GameObject g in GameObject.FindGameObjectsWithTag("SpawnLocation"))
        {
            spawnPositions.Add(g.transform);
        }
        spawnPositions.Sort(0, spawnPositions.Count, new ShuffleComparer<Transform>());

        for (int i = 0; i < 6; i++)
        {
            string enemyName = RandomName();
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

            SpawnEnemy(enemyName, position, enemyColor, enemyHP, hunger, enemyHP, damage, gold, seafoodAff, spicyAff, candyAff, veggieAff, sourAff);
        }
    }

    private void SpawnEnemy(string name, Vector3 position, Color color, int maxHP, int hunger, int currentHP, int dmg, int goldReward, int waterAff, int fireAff, int airAff, int earthAff, int electricAff)
    {
        // Sets components.
        GameObject newEnemy = Instantiate(enemy, position, Quaternion.identity);
        Player stats = newEnemy.GetComponent<Player>();
        Transform enemyTrans = newEnemy.GetComponent<Transform>();

        // Sets stats based on arguments.
        stats.name = name;
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

        stats.type = typeString + " Type";

        enemies.Add(newEnemy);
    }

    private Color RandomColor()
    {
        int colorCount = 9;
        int randomColor = Random.Range(0, colorCount);

        return (Color) randomColor;
    }

    private string RandomName()
    {
        int nameCount = 2;
        int randomName = Random.Range(0, nameCount);

        return ((Name) randomName).ToString();
    }

    private Vector3 RandomPosition()
    {
        return spawnPositions[listPos++].position;
    }
}
