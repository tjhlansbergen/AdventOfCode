import helpers

input = helpers.split_file_on_blanklines('inputs/day4')
passports = [block.replace('\n', ' ').split(' ') for block in input]

def part_one(passports):
    def validate_parsed_block(block):

        required = ['byr','iyr','eyr','hgt','hcl','ecl','pid']
        block_parts = [x[0:3] for x in block]

        for r in required:
            if r not in block_parts:
                return None
        return block

    
    validated_passports = [passport for passport in (validate_parsed_block(p) for p in passports) if passport is not None]

    return validated_passports

def part_two(passports):

    def key(part):
        return part.split(':')[0]
    def value(part):
        return part.split(':')[1]

    def valid_byr(part):
        if key(part) != 'byr':
            return True
        return 1920 <= int(value(part)) <= 2002

    def valid_iyr(part):
        if key(part) != 'iyr':
            return True
        return 2010 <= int(value(part)) <= 2020

    def valid_eyr(part):
        if key(part) != 'eyr':
            return True
        return 2020 <= int(value(part)) <= 2030

    def valid_hgt(part):
        if key(part) != 'hgt':
            return True
        unit = value(part)[len(value(part))-2:len(value(part))]
        height = value(part)[0:len(value(part))-2]
        if unit == 'cm':
            return 150 <= int(height) <= 193
        elif unit == 'in':
            return 59 <= int(height) <= 76
        else:
            return False

    def valid_hcl(part):
        if key(part) != 'hcl':
            return True
        if value(part)[0] != '#':
            return False
        if len(value(part)) != 7:
            return False
        for i in value(part)[1:6]:
            if i not in ['0','1','2','3','4','5','6','7','8','9','a','b','c','d','e','f']:
                return False
        return True

    def valid_ecl(part):
        if key(part) != 'ecl':
            return True
        return value(part) in ['amb', 'blu', 'brn', 'gry',  'grn', 'hzl', 'oth']

    def valid_pid(part):
        if key(part) != 'pid':
            return True
        return len(value(part)) == 9 and value(part).isnumeric()

    def validate_passport(passport):
        for part in passport:
            tests = [valid_byr(part), valid_ecl(part), valid_eyr(part),valid_hcl(part),valid_hgt(part),valid_iyr(part),valid_pid(part)]
            if not all(tests):
                return None
        return passport


    
    scanned = part_one(passports)
    valid = [passport for passport in (validate_passport(p) for p in scanned) if passport is not None]
    return valid

print(len(part_one(passports)))
print(len(part_two(passports)))
