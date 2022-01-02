import helpers

grid = helpers.list_from_file('inputs\day3')

# rotate the grid (90* clockwise)
rotated = list(zip(*reversed(grid)))

# sum the columns (now rows)
sums = [x.count('1') for x in rotated]

# if the sum exceeds the number of items / 2 a 1 is most common
most_common = "".join('1' if s > len(grid)/2 else '0' for s in sums)

# invert the binary
least_common = "".join('1' if x == '0' else '0' for x in most_common)


# part one

# convert to int and multiply
print(int(most_common, 2) * int(least_common, 2))


# part two: I'm sure that are better solutions for this, but it ain't stupid if it works

def most_least_common_bit_at_index(grid, index: int) -> int:
    rotated = list(zip(*reversed(grid)))
    sum = rotated[index].count('1')
    if sum == len(grid)/2:
        return ("1", "0")
    elif sum > len(grid)/2:
        return ("1","0")
    else:
        return ("0","1")

most_common = least_common = grid

for i in range(len(grid[0])):
    most_common_bit = most_least_common_bit_at_index(most_common, i)[0]
    least_common_bit = most_least_common_bit_at_index(least_common, i)[1]
    if len(most_common) > 1:
        most_common = [x for x in most_common if x[i] == most_common_bit]
    if len(least_common) > 1:
        least_common = [x for x in least_common if x[i] == least_common_bit]

# convert to int and multiply
print(int(most_common[0], 2) * int(least_common[0], 2))