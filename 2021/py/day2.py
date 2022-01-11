import helpers

lines = helpers.list_from_file('2021/inputs/day2')

# part one
h = 0
d = 0

for line in lines:
    direction = line.split(" ")[0]
    amount = int(line.split(" ")[1])
    if direction == "forward":
        h += amount
    if direction == "down":
        d += amount
    if direction == "up":
        d -= amount

print(h*d)

# part two
h = 0
d = 0
a = 0

for line in lines:
    direction = line.split(" ")[0]
    amount = int(line.split(" ")[1])
    if direction == "forward":
        h += amount
        d += a*amount
    if direction == "down":
        a += amount
    if direction == "up":
        a -= amount

print(h*d)
