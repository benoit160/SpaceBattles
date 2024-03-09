using SpaceBattles.Core.Domain.Enums;
using SpaceBattles.Core.Domain.ValueTypes;

namespace SpaceBattles.Core.Domain.Entities.Battle;

public sealed class Spaceship : CombatEntity
{
    public SpaceshipsCharacteristics Characteristics { get; init; }

    public int BaseSpeed { get; init; }

    public int CargoCapacity { get; init; }

    public int FuelUsage { get; init; }
    
    public static Spaceship[] Spaceships() 
    {
        return
        [
            new Spaceship
            {
                Id = 11,
                Name = "Light fighter",
                ImageName = "light_fighter.webp",
                Description = "This is the first fighting ship all emperors will build. The light fighter is an agile ship, but vulnerable when it is on its own. In mass numbers, they can become a great threat to any empire. They are the first to accompany small and large cargoes to hostile planets with minor defences.",

                Characteristics = SpaceshipsCharacteristics.Combustion | SpaceshipsCharacteristics.Fighters,

                BaseSpeed = 12_500,
                FuelUsage = 20,
                CargoCapacity = 50,

                BaseArmor = 400,
                BaseShield = 10,
                BaseWeaponPower = 50,

                TitaniumCost = 3_000,
                SiliconCost = 1_000,
                
                BuildingRequirements = 
                [
                    new BuildingRequirement(8, 1),
                ],
            },
            new Spaceship
            {
                Id = 12,
                Name = "Light cargo",
                ImageName = "small_cargo.webp",
                Description = "This is the first fighting ship all emperors will build. The light fighter is an agile ship, but vulnerable when it is on its own. In mass numbers, they can become a great threat to any empire. They are the first to accompany small and large cargoes to hostile planets with minor defences.",

                Characteristics = SpaceshipsCharacteristics.Combustion | SpaceshipsCharacteristics.Utility,

                BaseSpeed = 5_000,
                FuelUsage = 10,
                CargoCapacity = 5000,

                BaseArmor = 400,
                BaseShield = 10,
                BaseWeaponPower = 5,
                
                TitaniumCost = 2_000,
                SiliconCost = 2_000,
                
                BuildingRequirements = 
                [
                    new BuildingRequirement(8, 2),
                ],
            },
            new Spaceship
            {
                Id = 13,
                Name = "Large cargo",
                ImageName = "large_cargo.webp",
                Description = "Compared to the Small Cargo Ship, the Large Cargo has five times the cargo capacity for only three times the cost, and is 50% faster than a Combustion Drive powered small cargo ship",

                Characteristics = SpaceshipsCharacteristics.Combustion | SpaceshipsCharacteristics.Utility,

                BaseSpeed = 7_500,
                FuelUsage = 50,
                CargoCapacity = 25_000,

                BaseArmor = 1200,
                BaseShield = 25,
                BaseWeaponPower = 5,
                
                TitaniumCost = 6_000,
                SiliconCost = 6_000,
                
                BuildingRequirements = 
                [
                    new BuildingRequirement(8, 4),
                ],
            },
            new Spaceship
            {
                Id = 14,
                Name = "Heavy fighter",
                ImageName = "heavy_fighter.webp",
                Description = "In developing the heavy fighter, researchers reached a point at which conventional drives no longer provided sufficient performance. In order to move the ship optimally, the impulse drive was used for the first time. This increased the costs, but also opened new possibilities. By using this drive, there was more energy left for weapons and shields; in addition, high-quality materials were used for this new family of fighters. With these changes, the heavy fighter represents a new era in ship technology and is the basis for cruiser technology.",

                Characteristics = SpaceshipsCharacteristics.Impulse | SpaceshipsCharacteristics.Fighters,

                BaseSpeed = 10_000,
                FuelUsage = 75,
                CargoCapacity = 100,

                BaseArmor = 1_000,
                BaseShield = 25,
                BaseWeaponPower = 150,
                
                TitaniumCost = 6_000,
                SiliconCost = 4_000,
                
                BuildingRequirements = 
                [
                    new BuildingRequirement(8, 3),
                ],
            },
            new Spaceship
            {
                Id = 15,
                Name = "Cruiser",
                ImageName = "cruiser.webp",
                Description = "With the development of the heavy laser and the ion cannon, light and heavy fighters encountered an alarmingly high number of defeats that increased with each raid. Despite many modifications, weapons strength and armour changes, it could not be increased fast enough to effectively counter these new defensive measures. Therefore, it was decided to build a new class of ship that combined more armor and more firepower. As a result of years of research and development, the Cruiser was born.\r\n\r\nCruisers are armored almost three times of that of the heavy fighters, and possess more than twice the firepower of any combat ship in existence. They also possess speeds that far surpassed any spacecraft ever made. For almost a century, cruisers dominated the universe. However, with the development of Gauss cannons and plasma turrets, their predominance ended. They are still used today against fighter groups, but not as predominantly as before.",

                Characteristics = SpaceshipsCharacteristics.Impulse | SpaceshipsCharacteristics.Fighters,

                BaseSpeed = 15_000,
                FuelUsage = 300,
                CargoCapacity = 800,

                BaseArmor = 2_700,
                BaseShield = 50,
                BaseWeaponPower = 400,
                
                TitaniumCost = 20_000,
                SiliconCost = 7_000,
                HeliumCost = 2_000,
                
                BuildingRequirements = 
                [
                    new BuildingRequirement(8,5),
                ],
            },
            new Spaceship
            {
                Id = 16,
                Name = "Battleship",
                ImageName = "battleship.webp",
                Description = "Once it became apparent that the cruiser was losing ground to the increasing number of defense structures it was facing, and with the loss of ships on missions at unacceptable levels, it was decided to build a ship that could face those same type of defense structures with as little loss as possible. After extensive development, the Battleship was born. Built to withstand the largest of battles, the Battleship features large cargo spaces, heavy cannons, and high hyperdrive speed. Once developed, it eventually turned out to be the backbone of every raiding Emperors fleet.",

                Characteristics = SpaceshipsCharacteristics.Hyperspace | SpaceshipsCharacteristics.Warships,

                BaseSpeed = 10_000,
                FuelUsage = 500,
                CargoCapacity = 1500,

                BaseArmor = 6_000,
                BaseShield = 200,
                BaseWeaponPower = 1000,
                
                TitaniumCost = 45_000,
                SiliconCost = 15_000,
                
                BuildingRequirements = 
                [
                    new BuildingRequirement(8, 7),
                ],
            },
            new Spaceship
            {
                Id = 17,
                Name = "Battlecruiser",
                ImageName = "battlecruiser.webp",
                Description = "This ship is one of the most advanced fighting ships ever to be developed, and is particularly deadly when it comes to destroying attacking fleets. With its improved laser cannons on board and advanced Hyperspace engine, the Battlecruiser is a serious force to be dealt with in any attack. Due to the ships design and its large weapons system, the cargo holds had to be cut, but this is compensated for by the lowered fuel consumption.",

                Characteristics = SpaceshipsCharacteristics.Hyperspace | SpaceshipsCharacteristics.Warships,

                BaseSpeed = 10_000,
                FuelUsage = 250,
                CargoCapacity = 750,

                BaseArmor = 7_000,
                BaseShield = 400,
                BaseWeaponPower = 700,
                
                TitaniumCost = 30_000,
                SiliconCost = 40_000,
                HeliumCost = 15_000,
                
                BuildingRequirements = 
                [
                    new BuildingRequirement(8, 8),
                ],
            },
            new Spaceship
            {
                Id = 18,
                Name = "Bomber",
                ImageName = "bomber.webp",
                Description = "Over the centuries, as defenses were starting to get larger and more sophisticated, fleets were starting to be destroyed at an alarming rate. It was decided that a new ship was needed to break defenses to ensure maximum results. After years of research and development, the Bomber was created.\r\n\r\nUsing laser-guided targeting equipment and Plasma Bombs, the Bomber seeks out and destroys any defense mechanism it can find. As soon as the hyperspace drive is developed to Level 8, the Bomber is retrofitted with the hyperspace engine and can fly at higher speeds.",

                Characteristics = SpaceshipsCharacteristics.Impulse | SpaceshipsCharacteristics.Warships,

                BaseSpeed = 4_000,
                FuelUsage = 700,
                CargoCapacity = 500,

                BaseArmor = 7_500,
                BaseShield = 500,
                BaseWeaponPower = 1_000,
                
                TitaniumCost = 50_000,
                SiliconCost = 25_000,
                HeliumCost = 15_000,
                
                BuildingRequirements = 
                [
                    new BuildingRequirement(8, 8),
                ],
            },
            new Spaceship
            {
                Id = 19,
                Name = "Destroyer",
                ImageName = "destroyer.webp",
                Description = "The Destroyer is the result of years of work and development. With the development of Deathstars, it was decided that a class of ship was needed to defend against such a massive weapon.Thanks to its improved homing sensors, multi-phalanx Ion cannons, Gauss Cannons and Plasma Turrets, the Destroyer turned out to be one of the most fearsome ships created.\r\n\r\nBecause the destroyer is very large, its maneuverability is severely limited, which makes it more of a battle station than a fighting ship. The lack of maneuverability is made up for by its sheer firepower, but it also costs significant amounts of deuterium to build and operate.",

                Characteristics = SpaceshipsCharacteristics.Hyperspace | SpaceshipsCharacteristics.Warships,

                BaseSpeed = 5_000,
                FuelUsage = 1_000,
                CargoCapacity = 2_000,

                BaseArmor = 11_000,
                BaseShield = 500,
                BaseWeaponPower = 2_000,
                
                TitaniumCost = 60_000,
                SiliconCost = 50_000,
                HeliumCost = 15_000,
                
                BuildingRequirements = 
                [
                    new BuildingRequirement(8, 9),
                ],
            },
            new Spaceship
            {
                Id = 20,
                Name = "Deathstar",
                ImageName = "deathstar.webp",
                Description = "The Deathstar is the most powerful ship ever created. This moon sized ship is the only ship that can be seen with the naked eye on the ground. By the time you spot it, unfortunately, it is too late to do anything. Armed with a gigantic graviton cannon, the most advanced weapons system ever created in the Universe, this massive ship has not only the capability of destroying entire fleets and defences, but also has the capability of destroying entire moons. Only the most advanced empires have the capability to build a ship of this mammoth size.",

                Characteristics = SpaceshipsCharacteristics.Hyperspace | SpaceshipsCharacteristics.Warships,

                BaseSpeed = 100,
                FuelUsage = 1,
                CargoCapacity = 1_000_000,

                BaseArmor = 900_000,
                BaseShield = 50_000,
                BaseWeaponPower = 200_000,
                
                TitaniumCost = 5_000_000,
                SiliconCost = 4_000_000,
                HeliumCost = 1_000_000,
                
                BuildingRequirements = 
                [
                    new BuildingRequirement(8, 12),
                ],
            },
            new Spaceship
            {
                Id = 21,
                Name = "Recycler",
                ImageName = "recycler.webp",
                Description = "Recyclers are used to harvest resources from debris fields which are created whenever combat resulting in the destruction of ships occurs at a planet.",
            
                Characteristics = SpaceshipsCharacteristics.Combustion | SpaceshipsCharacteristics.Utility,
            
                BaseSpeed = 2_000,
                FuelUsage = 300,
                CargoCapacity = 20_000,
            
                BaseArmor = 1_600,
                BaseShield = 10,
                BaseWeaponPower = 1,
                
                TitaniumCost = 10_000,
                SiliconCost = 6_000,
                HeliumCost = 2_000,
                
                BuildingRequirements = 
                [
                    new BuildingRequirement(8, 4),
                ],
            },
            new Spaceship
            {
                Id = 22,
                Name = "Espionage probe",
                ImageName = "espionage_probe.webp",
                Description = "Recyclers are used to harvest resources from debris fields which are created whenever combat resulting in the destruction of ships occurs at a planet.",
            
                Characteristics = SpaceshipsCharacteristics.Combustion | SpaceshipsCharacteristics.Utility,
            
                BaseSpeed = 100_000_000,
                FuelUsage = 1,
                CargoCapacity = 5,
            
                BaseArmor = 100,
                BaseShield = 0,
                BaseWeaponPower = 0,
                
                SiliconCost = 1_000,
                
                BuildingRequirements = 
                [
                    new BuildingRequirement(8, 3),
                ],
            },
            new Spaceship
            {
                Id = 23,
                Name = "Solar satellite",
                ImageName = "solar_satellite.webp",
                Description = "Recyclers are used to harvest resources from debris fields which are created whenever combat resulting in the destruction of ships occurs at a planet.",
            
                Characteristics = SpaceshipsCharacteristics.Combustion | SpaceshipsCharacteristics.Utility,
            
                BaseSpeed = 0,
                FuelUsage = 0,
                CargoCapacity = 0,
            
                BaseArmor = 200,
                BaseShield = 1,
                BaseWeaponPower = 1,
                
                SiliconCost = 2_000,
                HeliumCost = 500,
                
                BuildingRequirements = 
                [
                    new BuildingRequirement(8, 1),
                ],
            },
        ];
    }
}