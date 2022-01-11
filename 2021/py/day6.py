import helpers

ages = [int(age) for age in helpers.list_from_file('2021/inputs/day6')[0].split(",")] 




def spawn_fish(generations):        
    days = 0
    while days < generations:

        carryover = fish[0]
        for i in range(9):
            if i < 6:
                fish[i] = fish[i+1]
            elif i == 6:
                fish[i] = fish[i+1] + carryover
            elif i == 7:
                fish[i] = fish[i+1]
            elif i == 8:
                fish[i] = carryover

        days += 1

# part one

fish = {}       # no need to known about every other fish, I can group them by age
for i in range(9):
    fish[i] = ages.count(i)

spawn_fish(80)
print(sum(fish.values()))

# part two

fish = {}       # no need to known about every other fish, I can group them by age
for i in range(9):
    fish[i] = ages.count(i)

spawn_fish(256)
print(sum(fish.values()))