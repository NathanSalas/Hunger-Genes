# Hunger-Genes

Final project for Computer science degree. A game that uses genetic algorithms to control the non-player opponents.

Makes use of the Monogame framework (https://www.monogame.net/)

### Design

3 populations of enemies navigate to the center of the map through genetic algorithms. The top two individuals from each population are used to create the next generation.

Once any indivudual reaches the center, the map is reset. As the game goes on, the populations will navigate to the center quicker and quicker.
