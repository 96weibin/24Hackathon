export const getNonBasisType = (number) => {
    switch (number) {
      case 1:
        return "Purchase";
      case 2:
        return "Sales";
      case 3:
        return "Capacity";
      case 4:
        return "ProcLimit";
      default:
        return "All";
    }
}