import helpers

measurements = helpers.list_int_from_file('2021/inputs/day1')

# part one
print(len([i for i in range(len(measurements)-1) if measurements[i] < measurements[i+1]]))

# part two
print(len([i for i in range(len(measurements)-3) if sum(measurements[i:i+3]) < sum(measurements[i+1:i+4])]))