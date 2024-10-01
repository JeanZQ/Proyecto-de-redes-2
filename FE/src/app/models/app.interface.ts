export const DEFAULT_PASSWORD = 'Shazam!';


export interface ServerGameResponse {
    status: number;
    msg: string;
    data: GameResponse[];
    other: any;
};

export interface GameResponse {
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

export interface NewGame {
    name: string;
    owner: string;
    password?: string;
};

export interface JoinGame {
    id: string;
    player?: string;
    owner: string;
    password: string;
};


export interface StartGame {
    id: string;
    player: string;
    password: string;
};

export interface SearchGame {
    name: string;
    status: string;
    page: number;
    limit: number;
};

export interface GameInfo{
    id:string;
    player:string;
    owner:string;
    password:string;
};




