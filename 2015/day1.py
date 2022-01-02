import helpers

lines = helpers.list_from_file('2015/inputs/day1')

for line in lines:
    print(line.count('(') - line.count(')'))
