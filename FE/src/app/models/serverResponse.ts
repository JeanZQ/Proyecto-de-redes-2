export type ServerResponse = {
    status: number;
    msg: string;
    data: GameResponse[];
    other: any;
};

export type GameResponse = {
    name: string;
    owner: string;
    status: string;
    createdAt: string;
    updatedAt: string;
    password: boolean;
    players: string[];
    enemies: string[];
    currentRound: string;
    id: string;
};