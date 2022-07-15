"""Advent of Code 2016 - Puzzle 5"""

import hashlib


def solve_part_1() -> str:
    """Solve part 1 of the puzzle.

    Returns:
        The code as a string.
    """
    index = 0
    password = []
    while len(password) < 8:
        hex_string = hashlib.md5(bytes(f"reyedfim{str(index)}", "UTF-8")).hexdigest()
        if hex_string[:5] == "00000":
            password.append(hex_string[5])
        index += 1

    return "".join(password)


def solve_part_2() -> str:
    """Solve part 2 of the puzzle.

    Returns:
        The code as a string.
    """
    index = 0
    password = {}
    while len(password.keys()) < 8:
        hex_string = hashlib.md5(bytes(f"reyedfim{str(index)}", "UTF-8")).hexdigest()
        if (
            hex_string[:5] == "00000"
            and hex_string[5] not in password
            and hex_string[5] in list("01234567")
        ):
            password[hex_string[5]] = hex_string[6]
        index += 1

    return "".join(password[i] for i in list("01234567"))


def main() -> None:
    """Main function to run script."""
    answer_1 = solve_part_1()
    answer_2 = solve_part_2()
    print(f"Part 1 - {answer_1}")
    print(f"Part 2 - {answer_2}")


if __name__ == "__main__":
    main()
