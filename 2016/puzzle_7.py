"""Advent of Code 2016 - Puzzle 7"""

import re


def read_ips() -> list[str]:
    """Read IP addresses from text file.

    Returns:
        List of IP addresses.
    """
    with open("2016/puzzle_input/puzzle_7.txt", "r", encoding="utf-8") as file_handle:
        data = file_handle.readlines()
    return data


def check_abba(string: str) -> bool:
    """Check if a string contains an ABBA pattern.

    Args:
        string: The string to check.

    Returns:
        True if the string contains an ABBA pattern.
    """
    for i in range(len(string) - 3):
        s_4 = string[i : i + 4]
        if s_4 == s_4[::-1] and s_4[:2] != s_4[2:]:
            return True
    return False


def find_abas(string: str) -> list[str]:
    """Find ABA patterns in a string.

    Args:
        string: The string to find patterns in.

    Returns:
        List containing ABA patterns.
    """
    aba_list = []
    for i in range(len(string) - 2):
        s_3 = string[i : i + 3]
        if s_3 == s_3[::-1] and s_3[0] != s_3[1]:
            aba_list.append(s_3)
    return aba_list


def check_bab(bracket_list: list[str], aba_list: list[str]) -> bool:
    """Check if BAB of ABA pattern is in any string in the bracket list.

    Args:
        bracket_list: List of strings found inside brackets.
        aba_list: List of all ABA patterns found outside of brackets.

    Returns:
        True if any ABA pattern has a corresponding BAB pattern in any
        of the bracket strings.
    """
    for aba in aba_list:
        bab = aba[1] + aba[0] + aba[1]
        if any(bab in bracket_string for bracket_string in bracket_list):
            return True
    return False


class IP:
    """Class to represent an IP address.

    Args:
        address: The IP address.

    Attributes:
        address: The IP address.
        bracket_strings: List of strings inside the brackets.
        outer_strings: List of strings outside the brackets.
        abas: List of ABA patterns in outer strings.
    """

    def __init__(self, address: str):
        self.address = address

    @property
    def bracket_strings(self) -> list[str]:
        """Return strings inside of square brackets."""
        return re.findall("\[(\w+)\]", self.address)

    @property
    def outer_strings(self) -> list[str]:
        """Return strings outside of square brackets."""
        return [
            s
            for s in re.findall("[\w+]+", self.address)
            if s not in self.bracket_strings
        ]

    @property
    def abas(self) -> list[str]:
        """Return list of all ABA patterns."""
        return [s for string in self.outer_strings for s in find_abas(string)]

    def is_valid_aba(self) -> bool:
        """Check if an IP address is valid according to ABA check."""
        return check_bab(self.bracket_strings, self.abas)

    def is_valid_abba(self) -> bool:
        """Check if an IP address is valid according to ABBA check."""
        return any(check_abba(s) for s in self.outer_strings) and not any(
            check_abba(s) for s in self.bracket_strings
        )


def main() -> None:
    """Main fuction to run script."""
    ip_addresses = read_ips()
    total_valid_abba = sum(IP(ip).is_valid_abba() for ip in ip_addresses)
    total_valid_aba = sum(IP(ip).is_valid_aba() for ip in ip_addresses)

    print(f"Part 1\nNumber of IPs which support TLS - {total_valid_abba}")
    print(f"Part 2\nNumber of IPs which support SLS - {total_valid_aba}")


if __name__ == "__main__":
    main()
