"""Advent of Code 2016 - Puzzle 2"""

from dataclasses import dataclass


@dataclass
class Pad:
    """Class to represent number pad."""
    mappings: dict[tuple, int | str]

    def get_number(self, key: tuple) -> int:
        """Return value of button for a given position."""
        return self.mappings[key]

    @property
    def positions(self):
        """Return list of valid positions on number pad."""
        return list(self.mappings.keys())


NUM_PAD_1 = {
    (1, 3): 1,
    (2, 3): 2,
    (3, 3): 3,
    (1, 2): 4,
    (2, 2): 5,
    (3, 2): 6,
    (1, 1): 7,
    (2, 1): 8,
    (3, 1): 9,
}

NUM_PAD_2 = {
    (3, 5): 1,
    (2, 4): 2,
    (3, 4): 3,
    (4, 4): 4,
    (1, 3): 5,
    (2, 3): 6,
    (3, 3): 7,
    (4, 3): 8,
    (5, 3): 9,
    (2, 2): "A",
    (3, 2): "B",
    (4, 2): "C",
    (3, 1): "D",
}

MOVEMENTS = {"U": (0, 1), "D": (0, -1), "L": (-1, 0), "R": (1, 0)}


def read_instructions() -> list[str]:
    """Read instructions.

    Returns:
        List of instructions.
    """
    with open("2016/puzzle_input/puzzle_2.txt", "r", encoding="utf-8") as file_handle:
        instructions = file_handle.read().split("\n")
    return instructions


def solve(
    instructions: list[str],
    start_position: tuple[int, int],
    num_pad: Pad,
) -> str:
    """Move

    Args:
        instructions: List of instructions.
        start_positions: Tuple with starting position.
        num_pad: The number pad to move over.

    Returns:
        The 5 character code.
    """
    current_position = start_position
    code = []

    for i in instructions:
        for movement in i:
            next_position = tuple(map(sum, zip(current_position, MOVEMENTS[movement])))
            if next_position in num_pad.positions:
                current_position = next_position
        code.append(str(num_pad.get_number(current_position)))
    return "".join(code)


def main():
    """Main function to run script."""
    instructions = read_instructions()
    code_1 = solve(instructions, (2, 2), Pad(NUM_PAD_1))
    code_2 = solve(instructions, (1, 3), Pad(NUM_PAD_2))
    print(f"Part 1 - {code_1}")
    print(f"Part 2 - {code_2}")


if __name__ == "__main__":
    main()
