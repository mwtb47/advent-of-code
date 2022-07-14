"""Advent of Code 2016 - Puzzle 1"""

from dataclasses import dataclass


@dataclass(unsafe_hash=True)
class Direction:
    """Directional vector with magnitude 1."""
    x: int
    y: int

    def turn(self, direction: str):
        """Rotate the directional vector clockwise or anti-clockwise."""
        self.x, self.y = (self.y, -self.x) if direction == "R" else (-self.y, self.x)


@dataclass(unsafe_hash=True)
class Position:
    """Positional vector."""
    x: int
    y: int

    def move(self, direction: Direction) -> None:
        """Move to a new position."""
        self.x += direction.x
        self.y += direction.y

    @property
    def blocks(self):
        """Return the number of blocks from the origin."""
        return abs(self.x) + abs(self.y)


def read_instructions() -> list[str]:
    """Read instructions.

    Returns:
        List of instructions.
    """
    with open("2016/puzzle_input/puzzle_1.txt", "r", encoding="utf-8") as file_handle:
        instructions = file_handle.read().replace("\n", "").split(", ")
    return instructions


def move(instructions: list[str]) -> tuple[Position, list[Position]]:
    """Move according to each instruction.

    Args:
        instructions: List of instructions.

    Returns:
        A tuple, the first item being the final position, the
        second item being a list of all waypoints.
    """
    movement = Direction(0, 1)
    position = Position(0, 0)
    waypoints = []
    for i in instructions:
        movement.turn(i[0])
        for _ in range(int(i[1:])):
            position.move(movement)
            waypoints.append(Position(position.x, position.y))
    return position, waypoints


def get_first_retrace(waypoints: list[Position]) -> Position | None:
    """Find the first retraced waypoint.

    Args:
        waypoints: List of Position objects.

    Returns:
        The first position which has already occured in the list.
    """
    already_visited = set()
    for waypoint in waypoints:
        if waypoint in already_visited:
            return waypoint
        already_visited.add(waypoint)
    return None


def main():
    """Main function to run script."""
    instructions = read_instructions()
    final_position, waypoints = move(instructions)
    first_retrace = get_first_retrace(waypoints)

    blocks_away_final = final_position.blocks
    blocks_away_retrace = first_retrace.blocks

    print(f"Part 1\nBlocks away, final positon - {blocks_away_final}\n")
    print(f"Part 2\nBlocks away, first retrace - {blocks_away_retrace}")


if __name__ == "__main__":
    main()
