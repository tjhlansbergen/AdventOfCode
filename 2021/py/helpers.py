import urllib.request

# gets a file from its URL
def getfile(url: str):
    return urllib.request.urlopen(url)

# gets a list from the lines in a file
def list_from_file(path: str):
    with open(path) as file:
        lines = file.readlines()
        return [line.rstrip() for line in lines]

# gets a list of int's from a file
def list_int_from_file(path: str):
    return [int(line) for line in list_from_file(path)]

# get a list of blocks seperated by one line of whitespace
def split_file_on_blanklines(path: str):
    with open(path) as file:
        blocks = file.read().split('\n\n')
        return blocks

def split_file_on_comma(path: str):
    with open(path) as file:
        return file.split(",")

def split_int_file_on_comma(path: str):
    with open(path) as file:
        splits = file.read().split(",")
        return [int(split) for split in splits]