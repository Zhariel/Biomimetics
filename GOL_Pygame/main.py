import pygame
from random import randint as r

speed_mult = 1
width = 100
size = 10
screen = pygame.display.set_mode((width*size, width*size))
clock = pygame.time.Clock()
col1 = pygame.Color("#edb4a1")
col2 = pygame.Color("#2c2137")
neighbours = [(-1, -1), (0, -1), (1, -1),  (-1, 0), (1, 0),  (-1, 1), (0, 1), (1, 1)]


def update_board(board):
    b = [i.copy() for i in board]

    for i in range(width):
        for j in range(width):
            b[i][j] = checklife(board, i, j, board[i][j])

    return b

def checklife(b, y, x, cur):
    count = 0
    for n in neighbours:
        if y+n[0]<0 or y+n[0]>width-1 or x+n[1]<0 or x+n[1]>width-1:
            continue
        checked = b[y+n[0]][x+n[1]]
        count += checked / (1 if checked == 0 else checked)
    if cur == 1 and (count == 2 or count == 3):
        return 1
    elif cur == 0 and count == 3:
        return 1
    return 0

def draw(board):
    for i in range(width):
        for j in range(width):
            color = col1 if board[i][j] == 1 else col2
            pygame.draw.rect(screen, color, (i*size, j*size, size, size))

def main():
    board = [[r(0, 1) for y in range(width)] for x in range(width)]
    pygame.init()

    while True:
        for event in pygame.event.get():
            if event.type == pygame.QUIT:
                pygame.quit()
                break

        # clock.tick(120)
        screen.fill(col2)
        draw(board)
        board = update_board(board)
        pygame.display.flip()

main()