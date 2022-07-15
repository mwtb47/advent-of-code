"""Advent of Code 2016 - Puzzle 4"""

import re

import pandas as pd


def read_input() -> list[str]:
    """Read in input.

    Returns:
        List of encrypted data.
    """
    with open("2016/puzzle_input/puzzle_4.txt", "r", encoding="utf-8") as file_handle:
        data = file_handle.read().split("\n")
    return data


def count_real_rooms(codes: list[str]) -> int:
    """Count the number of real rooms.

    Args:
        codes: List of codes.

    Return:
        The total number of real rooms.
    """
    total = 0

    for code in codes:
        letters = re.findall("[a-z]", code[: code.index("[")])
        number = re.findall("\-(\d+)\[", code)[0]
        check = re.findall("\[(\w+)\]", code)[0]

        df = (
            pd.DataFrame({"letters": letters})
            .groupby("letters", as_index=False)
            .size()
            .sort_values(["size", "letters"], ascending=[False, True])
        )
        if "".join(df["letters"][:5]) == check:
            total += int(number)
    return total


def find_sector_id(codes: list[str]) -> str:
    """Solve part 2.

    Args:
        codes: List of codes.

    Returns:
        String to be printed out which contains the answer.

    Raises:
        ValueError: If the list of codes does not contain the answer.
    """
    alphabet = "abcdefghijklmnopqrstuvwxyz" * 2

    for code in codes:
        input_ = code.rsplit("-", 1)[0]
        number = re.findall("\-([0-9]+)\[", code)[0]
        shift = int(number) % 26
        output = "".join(
            alphabet[alphabet.index(i) + shift] if i != "-" else " " for i in input_
        )
        if "northpole" in output:
            return f"Sector ID for {output} - {number}"
    raise ValueError("List of codes does not contain the answer.")


def main() -> None:
    """Main function to run script."""
    codes = read_input()
    answer_1 = count_real_rooms(codes)
    answer_2 = find_sector_id(codes)
    print(f"Part 1\nNumber of real rooms - {answer_1}\n")
    print(f"Part 2\nNumber of real rooms - {answer_2}")


if __name__ == "__main__":
    main()
