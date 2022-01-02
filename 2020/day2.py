import helpers
import sys

lines = helpers.list_from_file('inputs/day2')

def long():

    def parse_line(line: str):
        return line.replace(':', '').split()

    def validate_parsed_line(parsed_line: tuple):
        min_max = parsed_line[0].split('-')
        min = int(min_max[0])
        max = int(min_max[1])
        letter = parsed_line[1]
        string = parsed_line[2]
        count = string.count(letter)
        return count >= min and count <= max
            

    parsed_lines = [parse_line(line) for line in lines]
    validated_lines = [validate_parsed_line(line) for line in parsed_lines]

    print(validated_lines.count(True))

def short():
    def validate_line(line: str):
        parts = line.replace(':', ' ').replace('-', ' ').split()
        return int(parts[0]) <= parts[3].count(parts[2]) <= int(parts[1]) 

    print([validate_line(line) for line in lines].count(True))

def part_two():

    def validate_line(line: str):
        parts = line.replace(':', ' ').replace('-', ' ').split()
        first, second, letter, string = parts[0], parts[1], parts[2], parts[3]
        return (string[int(first)-1] == letter) ^ (string[int(second)-1] == letter)

    print([validate_line(line) for line in lines].count(True))

long()
short()
part_two()