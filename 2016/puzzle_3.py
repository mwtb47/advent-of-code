"""Advent of Code 2016 - Puzzle 3"""

from itertools import product


def read_triangles() -> str:
    """Read input.

    Returns:
        Str of
    """
    with open("2016/puzzle_input/puzzle_3.txt", "r", encoding="utf-8") as file_handle:
        potential_triangles = [
            [int(side) for side in line.split()] for line in file_handle
        ]
    return potential_triangles


def count_valid_triangles_1(potential_triangles: list[list[int]]) -> int:
    """Count the number of valid triangles line by line.

    Args:
        potential_triangles: List of potential triangles.

    Returns:
        The number of valid triangles in the potential triangles list.
    """
    return sum(sum(pt) - max(pt) > max(pt) for pt in potential_triangles)


def count_valid_triangles_2(potential_triangles: list[list[int]]) -> int:
    """Count the number of valid triangles column-wise.

    Args:
        potential_triangles: List of potential triangles.

    Returns:
        The number of valid triangles in the potential triangles list.
    """
    valid_triangles = 0

    for column, row in product(range(3), range(0, len(potential_triangles), 3)):
        values = [potential_triangles[row + i][column] for i in range(3)]
        if sum(values) - max(values) > max(values):
            valid_triangles += 1
    return valid_triangles


def main() -> None:
    """Main function to run script."""
    potential_triangles = read_triangles()
    answer_1 = count_valid_triangles_1(potential_triangles)
    answer_2 = count_valid_triangles_2(potential_triangles)
    print(f"Part 1\nNumber of valid triangles - {answer_1}")
    print(f"Part 2\nNumber of valid triangles - {answer_2}")


if __name__ == "__main__":
    main()
