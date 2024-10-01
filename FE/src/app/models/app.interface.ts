import exp from "constants";

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
    password?: string;
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

// lo que manda el jugador en el request
export interface AllRoundsInfoRequest{
    gameId:string;
    password?:string;
    player:string;
};


// lo que manda el jugador en el request
export interface RoundInfoRequest{
    gameId:string;
    roundId:string;
    password?:string;
    player:string;
};


// muestra el contenido de uno o varios rounds
export interface RoundResponse {
    status: number;
    msg: string;
    data: RoundInfoData;
};


// lo que se le muestra al jugador acerca del round
export interface RoundInfoData{
    roundId:string;
    leader:string;
    status:string
    result:string;
    phase:string;
    group:string[]; // players
    votes:boolean[];
};

export interface ProposeRound{
    gameId:string;
    roundId:string;
    player:string;
    password?:string;
    group:string[];
};





