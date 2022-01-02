import helpers

lines = helpers.list_from_file('inputs/day12')
caves = {}

def dict_add_or_append(dct, key, value):
    if key == "end" or value == "start":
        return
    if key in dct:
        dct[key].append(value)
    else:
        dct[key] = [value]

# make cave system
for line in lines:
    frm = line.split('-')[0]
    to = line.split('-')[1]
    dict_add_or_append(caves, frm, to)
    dict_add_or_append(caves, to, frm)


def visit(cave, count, visited):
    if cave.islower():
        visited.append(cave)
    if cave == "end":
        count += 1
        return count
    for path in caves[cave]:
        if path not in visited:
            count = visit(path, count, visited)
            if path.islower():
                visited.pop()
            
    return count

def visit2(cave, count, visited, block):
    if cave.islower():
        if cave in visited:
            # 2nd visit, apply block
            block = True
        visited.append(cave)
    if cave == "end":
        count += 1
        return count
    for path in caves[cave]:
        if not block or path not in visited:
            count = visit2(path, count, visited, block)
            if path.islower():
                removed = visited.pop()
                if removed in visited:
                    # double item was removed, relieve block
                    block = False

    return count

print(visit("start", 0, []))
print(visit2("start", 0, [], False))