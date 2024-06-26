﻿namespace SpaceBattles.Core.Domain.Entities.Building;

using SpaceBattles.Core.Domain.Enums;
using SpaceBattles.Core.Domain.Interfaces;
using SpaceBattles.Core.Domain.Records;

public class Building : IBuildingRequirements
{
    public short Id { get; init; }

    public string Name { get; init; }
        = string.Empty;

    public string Description { get; init; }
        = string.Empty;

    public string ImageName { get; init; }
        = string.Empty;

    public string ImagePath => $"/images/buildings/{ImageName}";

    public int TitaniumCost { get; init; }

    public int SiliconCost { get; init; }

    public int HeliumCost { get; init; }

    public float ScalingFactor { get; init; }

    public ElectricalEntityStatus EnergyStatus { get; init; }

    public int BaseEnergy { get; set; }

    public BuildingRequirement[] BuildingRequirements { get; init; }
        = [];

    /// <summary>
    /// Contains the initial buildings list.
    /// </summary>
    public static Building[] Buildings()
    {
        return
        [
            new ProducerBuilding
            {
                Id = 1,
                ImageName = "titanium.webp",

                Name = "Titanium mine",
                Description = "The basis of your civilization’s growth and might is titanium ore, which is produced in the titanium mine. Titanium ore can be converted into titanium metal, which is the primary material for constructing and enhancing various structures and technologies.\nThe titanium mine has a flexible production, depending on its level and the depth of the drilling. The deeper the mine digs, the more ore it can produce, but also the more energy it needs. Titanium ore is copious and low-cost, but it is also essential for advancing your civilization and competing with other empires.\nThe titanium mine is the first building you should build and upgrade, as it will provide you with the foundation of your empire.",

                Resource = Resource.Titanium,
                ProductionFactor = 30,

                ScalingFactor = 1.5f,
                TitaniumCost = 60,
                SiliconCost = 15,

                EnergyStatus = ElectricalEntityStatus.Consummer,
                BaseEnergy = 10,
            },
            new StorageBuilding
            {
                Id = 2,
                ImageName = "titanium_storage.webp",

                Resource = Resource.Titanium,
                Name = "Titanium storage",
                Description = "Titanium Storage structures provide huge storage containment for raw unprocessed titanium from metal mines. The higher the level, the more titanium can be held.\nOnce the capacity is used up, no more titanium can be harvested by the metal mines.",

                ScalingFactor = 2f,
                TitaniumCost = 1000,
            },
            new ProducerBuilding
            {
                Id = 3,
                ImageName = "silicon.webp",

                Resource = Resource.Silicon,
                Name = "Silicon refinery",
                Description = "Silicon refineries supply the main resource used to produce electronic circuits and form certain alloy compounds.\nMining silicon crystals consumes some one and half times more energy than a mining titanium, making crystals more valuable.\nAlmost all ships and all buildings require silicon. Most silicon crystals required to build spaceships, however, are very rare, and like titanium can only be found at a certain depth. Therefore, building mines in deeper strata will increase the amount of silicon produced.",

                ProductionFactor = 20,

                ScalingFactor = 1.6f,
                TitaniumCost = 48,
                SiliconCost = 24,

                EnergyStatus = ElectricalEntityStatus.Consummer,
                BaseEnergy = 10,
            },
            new StorageBuilding
            {
                Id = 4,
                ImageName = "silicon_storage.webp",

                Resource = Resource.Silicon,
                Name = "Silicon storage",
                Description = "Silicon storage structures provide huge storage containment for unprocessed silicon crystals. The higher the level, the more silicon can be held.\nOnce the capacity is used up, no more silicon can be harvested by the mines.",

                ScalingFactor = 2f,
                TitaniumCost = 1000,
                SiliconCost = 500,
            },
            new ProducerBuilding
            {
                Id = 5,
                ImageName = "helium.webp",

                Resource = Resource.Helium,
                Name = "³Helium synthesizer",
                Description = "³Helium is a stable isotope of helium with a natural abundance in the oceans of colonies of approximately one atom in 6500 of helium (~154 PPM). Helium³ thus accounts for approximately 0.015% (on a weight basis, 0.030%) of all water.\nHelium³ is processed by special synthesizers which can separate the water from the Helium³ using specially designed centrifuges. The upgrade of the synthesizer allows for increasing the amount of Helium³ deposits processed.\nHelium³ is used when carrying out sensor phalanx scans, viewing galaxies, as fuel for ships, and performing specialized research upgrades.",

                ProductionFactor = 11,

                ScalingFactor = 1.5f,
                TitaniumCost = 225,
                SiliconCost = 75,

                EnergyStatus = ElectricalEntityStatus.Consummer,
                BaseEnergy = 20,
            },
            new StorageBuilding
            {
                Id = 6,
                ImageName = "helium_storage.webp",

                Resource = Resource.Helium,
                Name = "³Helium storage",
                Description = "³Helium Storage structures provide huge storage containment for ³He from the synthesizer. The higher the level, the more ³He can be held.\nOnce the capacity is used up, no more ³He can be refined by the synthesizer.",

                ScalingFactor = 2f,
                TitaniumCost = 1000,
                SiliconCost = 1000,
            },
            new()
            {
                Id = 7,
                ImageName = "solar_plant.webp",

                Name = "Solar plant",
                Description = "Each mine requires energy to function, which is produced primarily by the Solar Plant.",

                ScalingFactor = 1.5f,
                TitaniumCost = 75,
                SiliconCost = 30,

                EnergyStatus = ElectricalEntityStatus.Producer,
                BaseEnergy = 20,
            },
            new()
            {
                Id = 8,
                ImageName = "robotic_factory.webp",

                Name = "Robotics factory",
                Description = "The Robotics Factory primary goal is the production of State of the Art construction robots. Each upgrade to the robotics factory results in the production of faster robots, which is used to reduce the time needed to construct buildings.",

                ScalingFactor = 2f,
                TitaniumCost = 400,
                SiliconCost = 120,
                HeliumCost = 200,
            },
            new()
            {
                Id = 9,
                ImageName = "shipyard.webp",

                Name = "Shipyard",
                Description = "The planetary shipyard is responsible for the construction of spacecraft and defensive mechanisms. As the shipyard is upgraded, it can produce a wider variety of vehicles at a much greater rate of speed.\nIf a nanite factory is present on the planet, the speed at which ships are constructed is massively increased.",

                ScalingFactor = 2f,
                TitaniumCost = 400,
                SiliconCost = 200,
                HeliumCost = 100,

                BuildingRequirements =
                [
                    new BuildingRequirement(8, 2),
                ],
            },
            new()
            {
                Id = 10,
                ImageName = "research_lab.webp",

                Name = "Research lab",
                Description = "An essential part of any empire, Research Labs are where new technologies are discovered and older technologies are improved upon. With each level of the Research Lab constructed, the speed in which new technologies are researched is increased, while also unlocking newer technologies to research.\nIn order to conduct research as quickly as possible, research scientists are immediately dispatched to the colony to begin work and development. In this way, knowledge about new technologies can easily be disseminated throughout the empire.",

                ScalingFactor = 2f,
                TitaniumCost = 200,
                SiliconCost = 400,
                HeliumCost = 200,
            },
            new()
            {
                Id = 11,
                ImageName = "nanite_factory.webp",

                Name = "Nanite factory",
                Description = "Nanite Factory produces nano-sized robots, which are considered the ultimate achievement in Robotic technology. These are capable of greatly increasing construction speed of buildings, defenses and ships.",

                ScalingFactor = 2f,
                TitaniumCost = 1_000_000,
                SiliconCost = 500_000,
                HeliumCost = 100_000,

                BuildingRequirements =
                [
                    new BuildingRequirement(8, 10),
                ],
            },
            new()
            {
                Id = 12,
                ImageName = "missile_silo.webp",

                Name = "Missile silo",
                Description = "The missile silo is a building that launches and stores missiles.\nAnti-Ballistic Missiles are launched automatically whenever an approaching Interplanetary Missile is detected. Otherwise they do not take part in any attacks, thus they can not be sent on missions or destroyed by an attacking fleet.\nInterplanetary Missiles are your offensive weapon to destroy the defenses of your target. Using state of the art tracking technology, each missile targets a certain number of defenses for destruction. Tipped with an anti-matter bomb, they deliver a destructive force so severe that destroyed shields and defenses cannot be repaired.",

                ScalingFactor = 2f,
                TitaniumCost = 20_000,
                SiliconCost = 20_000,
                HeliumCost = 1_000,

                BuildingRequirements =
                [
                    new BuildingRequirement(9, 1),
                ],
            }
        ];
    }
}