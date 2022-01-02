import helpers
from typing import List
from typing import DefaultDict

class Bag:
  def __init__(self, name):
    self.name = name
    self.bags = {}

def add_or_get(list: List[Bag], bag_name: str) -> Bag:
    for bag in list:
        if bag.name == bag_name:
            return bag
    bag = Bag(bag_name)
    list.append(bag)
    return bag

def parse_rule(rule: str, existing_bags: List[Bag]) -> Bag:
    splits = rule.split("contain")
    bag = add_or_get(existing_bags, splits[0].replace("bags", "").strip())
    if splits[1].strip() == "no other bags.":
        #existing_bags.append(bag)
        return

    splits = splits[1].split(",")
    for part in splits:
        part = part.strip().replace(".", "")
        qty = int(part.split(" ", 1)[0])
        name = part.replace("bags", "").replace("bag", "").strip().split(" ", 1)[1]
        bag.bags[add_or_get(existing_bags, name)] = qty

def traverse_bag(bag: Bag, match: str, count: int) -> int:
    if bag.name == match:
        count += 1
    for bag in bag.bags:
        count = traverse_bag(bag, match, count)
    return count

def traverse_bags(bag: Bag, count: int) -> int:
    count += 1
    for bag, qty in bag.bags.items():
        for _ in range(qty):
            count = traverse_bags(bag, count)
    return count


rules = helpers.list_from_file('inputs/day7')
bags = []
for rule in rules:
    parse_rule(rule, bags)

# part one
match = "shiny gold"
bag_results = [traverse_bag(bag, match, 0) for bag in bags if bag.name != match]
print(len([result for result in bag_results if result > 0]))
pass

# part two
bag = next(b for b in bags if b.name == match)
count = -1  # compensate for the outermost shiny gold bag which weŕe not counting
print(traverse_bags(bag, count))