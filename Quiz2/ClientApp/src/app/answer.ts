export interface Answer {
    id: number;
    questionID: number;
    correct: boolean;
    text: string;
   marked?: boolean;
}
