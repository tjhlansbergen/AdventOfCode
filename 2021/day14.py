import helpers

blocks = helpers.split_file_on_blanklines('inputs/day14')
polymer = blocks[0]
insertions = {(i.split(' -> ')[0], i.split(' -> ')[1]) for i in blocks[1].split('\n')}

def step(polymer):
    insert = {}

    for i in range(len(polymer)-1):
        for pair in insertions:
            if pair[0] == f"{polymer[i]}{polymer[i+1]}":
                insert[i+1] = pair[1]

    reindex = 0
    for k, value in insert.items():
        i = k + reindex
        polymer = f"{polymer[:i]}{value}{polymer[i:]}"  
        reindex += 1


    return polymer

# part one
for i in range(10):
    polymer = step(polymer)

distinct = set(polymer)
counts = [polymer.count(d) for d in distinct]   # no need for the keys
print(max(counts) - min(counts))


# part two, like the lanternfish no need to keep the full string, just a list pairs with count
chars = {c: blocks[0].count(c) for c in set(blocks[0])}
pairs_in = [(blocks[0][p], blocks[0][p+1]) for p in range(len(blocks[0])-1)]
insertions = {(i.split(' -> ')[0][0], i.split(' -> ')[0][1]): i.split(' -> ')[1] for i in blocks[1].split('\n')}    #dict{tuple:char}

def add_or_update(dict, key, increment):
    if key in dict:
        dict[key] += increment
    else:
        dict[key] = increment

def insert(pair, count, pairs):
    if pair in insertions.keys():
        value = insertions[pair]
        add_or_update(chars, value, count)
        
        # form new pairs
        add_or_update(pairs, (pair[0], value), count)
        add_or_update(pairs, (value, pair[1]), count)

# starting dict of pair counts
pairs = {p: pairs_in.count(p) for p in set(pairs_in)}

# steps
for i in range(40):
    new_pairs = {}
    for pair, count in pairs.items():    
        insert(pair, count, new_pairs)
    pairs = new_pairs

print(max(chars.values()) - min(chars.values()))