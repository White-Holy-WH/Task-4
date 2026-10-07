using System;

class Program
{
    static void Main()
    {
        Random rnd = new Random();

        // Параметры героя
        int playerHp = 150;
        int maxPlayerHp = 150;

        int stamina = 75;
        int maxStamina = 75;

        // Количество использований восстановления выносливости
        int recoveryUses = 3;

        // Названия противников
        string[] enemies =
        {
            "Гоблин-разведчик",
            "Орочий вождь",
            "Дракон"
        };

        // Здоровье противников
        int[] enemyHp =
        {
            45,
            80,
            135
        };

        // Максимальное здоровье противников
        int[] enemyMaxHp =
        {
            45,
            80,
            135
        };

        // Урон противников
        int[] enemyDamage =
        {
            6,
            14,
            18
        };

        Console.WriteLine("======================================");
        Console.WriteLine("       КЛАССИЧЕСКОЕ ФЭНТЕЗИ");
        Console.WriteLine("======================================");
        Console.WriteLine("Вы — странствующий рыцарь.");
        Console.WriteLine("Вам предстоит сразиться с тремя врагами.");
        Console.WriteLine();

        for (int wave = 0; wave < enemies.Length; wave++)
        {
            string currentEnemy = enemies[wave];
            int currentEnemyHp = enemyHp[wave];
            int currentEnemyMaxHp = enemyMaxHp[wave];

            Console.WriteLine("--------------------------------------");
            Console.WriteLine($"ВОЛНА {wave + 1}: {currentEnemy}");
            Console.WriteLine($"Здоровье врага: {currentEnemyHp}");
            Console.WriteLine("--------------------------------------");

            bool victory = false;

            // Главный пошаговый игровой цикл
            while (playerHp > 0 && currentEnemyHp > 0)
            {
                Console.WriteLine();
                Console.WriteLine("============== СОСТОЯНИЕ ==============");

                // Полоса здоровья игрока
                Console.Write("Рыцарь  HP: [");

                int playerHpBars = playerHp / 5;

                for (int i = 0; i < playerHpBars; i++)
                {
                    Console.Write("#");
                }

                for (int i = playerHpBars; i < maxPlayerHp / 5; i++)
                {
                    Console.Write("-");
                }

                Console.WriteLine($"] {playerHp}/{maxPlayerHp}");

                // Полоса выносливости
                Console.Write("Выносливость: [");

                int staminaBars = stamina / 5;

                for (int i = 0; i < staminaBars; i++)
                {
                    Console.Write("#");
                }

                for (int i = staminaBars; i < maxStamina / 5; i++)
                {
                    Console.Write("-");
                }

                Console.WriteLine($"] {stamina}/{maxStamina}");

                // Полоса здоровья противника
                Console.Write($"{currentEnemy} HP: [");

                int enemyHpBars = currentEnemyHp * 20 / currentEnemyMaxHp;

                for (int i = 0; i < enemyHpBars; i++)
                {
                    Console.Write("#");
                }

                for (int i = enemyHpBars; i < 20; i++)
                {
                    Console.Write("-");
                }

                Console.WriteLine($"] {currentEnemyHp}/{currentEnemyMaxHp}");

                Console.WriteLine("========================================");

                int action;
                bool isValid;

                // Валидация выбора через do-while
                do
                {
                    Console.WriteLine();
                    Console.WriteLine("Выберите действие:");
                    Console.WriteLine("1 — Базовая атака");
                    Console.WriteLine("2 — Специальная способность");
                    Console.WriteLine("3 — Оборона");
                    Console.WriteLine("4 — Восстановление выносливости");
                    Console.Write("Ваш выбор: ");

                    isValid = int.TryParse(Console.ReadLine(), out action)
                              && action >= 1
                              && action <= 4;

                    if (!isValid)
                    {
                        Console.WriteLine("Ошибка! Введите число от 1 до 4.");
                    }

                } while (!isValid);

                bool defense = false;
                bool skipEnemyAttack = false;

                // Обработка действия игрока
                switch (action)
                {
                    case 1:
                        // Базовая атака
                        int basicDamage = rnd.Next(12, 18);

                        currentEnemyHp -= basicDamage;

                        if (currentEnemyHp < 0)
                        {
                            currentEnemyHp = 0;
                        }

                        Console.WriteLine();
                        Console.WriteLine(
                            $"Рыцарь наносит {basicDamage} урона!"
                        );

                        break;

                    case 2:
                        // Специальная способность
                        if (stamina >= 15)
                        {
                            int specialDamage = rnd.Next(25, 36);

                            currentEnemyHp -= specialDamage;
                            stamina -= 15;

                            if (currentEnemyHp < 0)
                            {
                                currentEnemyHp = 0;
                            }

                            Console.WriteLine();
                            Console.WriteLine(
                                $"Мощный удар! Рыцарь наносит {specialDamage} урона!"
                            );
                            Console.WriteLine(
                                "Потрачено 15 единиц выносливости."
                            );
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine(
                                "Недостаточно выносливости!"
                            );

                            // Переход к следующей итерации
                            continue;
                        }

                        break;

                    case 3:
                        // Оборона
                        defense = true;

                        Console.WriteLine();
                        Console.WriteLine(
                            "Рыцарь принимает защитную стойку."
                        );
                        Console.WriteLine(
                            "Получаемый урон будет уменьшен вдвое."
                        );

                        break;

                    case 4:
                        // Восстановление выносливости
                        if (recoveryUses > 0)
                        {
                            int restored = 20;

                            stamina += restored;

                            if (stamina > maxStamina)
                            {
                                stamina = maxStamina;
                            }

                            recoveryUses--;

                            Console.WriteLine();
                            Console.WriteLine(
                                $"Рыцарь восстановил {restored} выносливости."
                            );
                            Console.WriteLine(
                                $"Осталось использований: {recoveryUses}"
                            );
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine(
                                "Восстановление больше недоступно!"
                            );

                            continue;
                        }

                        break;
                }

                // Проверяем, побежден ли враг
                if (currentEnemyHp <= 0)
                {
                    Console.WriteLine();
                    Console.WriteLine($"*** {currentEnemy} повержен! ***");

                    victory = true;
                    break;
                }

                // Ход противника
                if (!skipEnemyAttack)
                {
                    int damage = rnd.Next(
                        enemyDamage[wave] - 2,
                        enemyDamage[wave] + 3
                    );

                    if (defense)
                    {
                        damage /= 2;

                        if (damage < 1)
                        {
                            damage = 1;
                        }

                        Console.WriteLine();
                        Console.WriteLine(
                            "Защита уменьшает получаемый урон!"
                        );
                    }

                    playerHp -= damage;

                    if (playerHp < 0)
                    {
                        playerHp = 0;
                    }

                    Console.WriteLine(
                        $"{currentEnemy} наносит {damage} урона!"
                    );

                    Console.WriteLine(
                        $"У рыцаря осталось {playerHp} HP."
                    );
                }

                // Проверяем, погиб ли игрок
                if (playerHp <= 0)
                {
                    Console.WriteLine();
                    Console.WriteLine("======================================");
                    Console.WriteLine("          РЫЦАРЬ ПОГИБ...");
                    Console.WriteLine("        ИГРА ОКОНЧЕНА");
                    Console.WriteLine("======================================");

                    return;
                }
            }

            // Если враг побежден — переходим к следующей волне
            if (victory)
            {
                Console.WriteLine();
                Console.WriteLine($"Волна {wave + 1} завершена!");

                // Небольшое восстановление между волнами
                playerHp += 10;

                if (playerHp > maxPlayerHp)
                {
                    playerHp = maxPlayerHp;
                }

                stamina += 10;

                if (stamina > maxStamina)
                {
                    stamina = maxStamina;
                }

                Console.WriteLine(
                    "Перед следующей битвой рыцарь немного восстановил силы."
                );
            }
        }

        // Если все три волны пройдены
        Console.WriteLine();
        Console.WriteLine("======================================");
        Console.WriteLine("       ПОБЕДА НАД ВСЕМИ ВРАГАМИ!");
        Console.WriteLine("======================================");
        Console.WriteLine("Странствующий рыцарь спас королевство!");
        Console.WriteLine($"Осталось здоровья: {playerHp}");
        Console.WriteLine($"Осталось выносливости: {stamina}");
    }
}
