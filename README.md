# Опис реалізації проекту

* Абстрактний клас Equipment визначає інтерфейс класу, об’єкти якого треба створювати, Конкретні класи Tank і Weapon представляють реалізацію класу Equipment : таких класів може бути безліч, бо ВПК створює та розроблює різні технології, такі як танки, зброя, оптика, деталі, літка, військові кораблі.. 
* Абстрактний клас MilitaryIndustrialComplex визначає абстрактний фабричний метод Create(), який повертає об'єкт Equipment.
Конкретні класи TankDeveloper і WeaponDeveloper - спадкоємці класу MilitaryIndustrialComplex, що визначають свою реалізацію методу Create (). Причому метод Create() кожного окремого класу-творця повертає певний конкретний тип продукту. Для кожного конкретного класу продукту визначається свій конкретний клас творця.
* Таким чином, клас MilitaryIndustrialComplex делегує створення об'єкта Equipment своїм спадкоємцям. А класи TankDeveloper  і WeaponDeveloper  можуть самостійно вибирати який конкретний тип продукту їм створювати.
